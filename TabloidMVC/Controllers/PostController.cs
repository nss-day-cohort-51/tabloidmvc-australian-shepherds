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
            if (User.IsInRole("Admin"))
            {
                var vm = new PostDropDownViewModel()
                {
                    Posts = _postRepository.ADMINGetAllPublishedPosts(),
                    UserIds = _userRepository.GetAll(),
                    CategoriesIds = _categoryRepository.GetAll()
                };

                vm.selectedCategory = 0;
                vm.selectedUser = 0;
                vm.UserIds.Add(new UserProfile()
                {
                    Id = 0,
                    FirstName = "-select-"
                });
                vm.CategoriesIds.Add(new Category()
                {
                    Id = 0,
                    Name = "-select-"
                });
                return View(vm);
            } else
            {
                var vm = new PostDropDownViewModel()
                {
                    Posts = _postRepository.GetAllPublishedPosts(),
                    UserIds = _userRepository.GetAll(),
                    CategoriesIds = _categoryRepository.GetAll()
                };

                vm.selectedCategory = 0;
                vm.selectedUser = 0;
                vm.UserIds.Add(new UserProfile()
                {
                    Id = 0,
                    FirstName = "-select-"
                });
                vm.CategoriesIds.Add(new Category()
                {
                    Id = 0,
                    Name = "-select-"
                });
                return View(vm);
            }
        }

        [HttpPost]
        public IActionResult Index(PostDropDownViewModel vm)
        {
            var Posts = new List<Post>();

            if(vm.selectedCategory != 0 && vm.selectedUser != 0)
            {
                Posts = _postRepository.GetUsersPublishedPostsByCategoryIdAndUserId(vm.selectedUser, vm.selectedCategory);
            }
            else if(vm.selectedUser != 0)
            {
                Posts = _postRepository.GetUsersPublishedPostsByUserId(vm.selectedUser);
            }
            else if(vm.selectedCategory != 0)
            {
                Posts = _postRepository.GetUsersPublishedPostsByCategoryId(vm.selectedCategory);
            }
            else
            {
                Posts = _postRepository.GetAllPublishedPosts();
            }

            var UserIds = _userRepository.GetAll();
            var CategoriesIds = _categoryRepository.GetAll();
            UserIds.Add(new UserProfile()
            {
                Id = 0,
                FirstName = "-select-"
            });
            CategoriesIds.Add(new Category()
            {
                Id = 0,
                Name = "-select-"
            });
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
            Console.WriteLine($"Post Id: {id}");
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
                Console.WriteLine(ex);
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
