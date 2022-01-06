using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;
using TabloidMVC.Models;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserProfileRepository _userRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, IUserProfileRepository userRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        
        public IActionResult Index()
        {

            var vm = new PostDropDownViewModel()
            {
                Posts = _postRepository.GetAllPublishedPosts(),
                UserIds = new MultiSelectList(_userRepository.GetAll(), "Id", "FullName"),
                CategoriesIds = new MultiSelectList(_categoryRepository.GetAll(), "Id", "Name")
            };  
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(PostDropDownViewModel vm)
        {
            var Posts = new List<Post>();

            if(vm.selectedCategories != null && vm.selectedUsers != null)
            {
                Posts = _postRepository.GetUsersPublishedPostsByCategoryIdAndUserId(vm.selectedUsers[0], vm.selectedCategories[0]);
            }
            else if(vm.selectedUsers != null)
            {
                Posts = _postRepository.GetUsersPublishedPostsByUserId(vm.selectedUsers[0]);
            }
            else if(vm.selectedCategories != null)
            {
                Posts = _postRepository.GetUsersPublishedPostsByCategoryId(vm.selectedCategories[0]);
            }
            else
            {
                Posts = _postRepository.GetAllPublishedPosts();
            }

            var UserIds = new MultiSelectList(_userRepository.GetAll(), "Id", "FullName");
            var CategoriesIds = new MultiSelectList(_categoryRepository.GetAll(), "Id", "Name"); 
            vm = new PostDropDownViewModel()
            {
                Posts = Posts,
                UserIds = UserIds,
                CategoriesIds = CategoriesIds,
            };
            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var post = _postRepository.GetPublishedPostById(id);
            if (post == null)
            {
                int userId = GetCurrentUserProfileId();
                post = _postRepository.GetUserPostById(id, userId);
                if (post == null)
                {
                    return NotFound();
                }
            }

            post.CurrentUserId = GetCurrentUserProfileId();

            return View(post);
        }

        public IActionResult Subscribe(int id)
        {

            try
            {

                var currentUserId = GetCurrentUserProfileId();
                var userId = _postRepository.GetAuthorIdByPostId(id);
                var currentDateTime = DateTime.Now;

                _postRepository.Subscribe(userId, currentUserId, currentDateTime);

                return RedirectToAction("Details", "Post", new { id = id });
            }
            catch(Exception ex)
            {
                return View();
            };

        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAll();
            return View(vm);
        }

        public IActionResult Edit(int id)
        {
            Post post = _postRepository.GetPublishedPostById(id);

            return View(post);
        }

        [HttpPost]
        public IActionResult Edit(Post post)
        {
            try
            {
                _postRepository.UpdatePost(post);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(post);
            }

            
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        public IActionResult Delete(int id)
        {
            _postRepository.Remove(id);
            return RedirectToAction("Index");
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
