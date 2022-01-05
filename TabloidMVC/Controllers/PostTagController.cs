using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class PostTagController : Controller
    {
        private readonly IPostTagRepository _postTagRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostRepository _postRepository;

        public PostTagController(IPostRepository postRepository, ITagRepository tagRepository, IPostTagRepository postTagRepository)
        {
            _postRepository = postRepository;
            _postTagRepository = postTagRepository;
            _tagRepository = tagRepository;

        }

        // GET: PostTagController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PostTagController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostTagController/Create
        public ActionResult Create(int id)
        {
            IEnumerable<Tag> tags = _tagRepository.GetAllTags();

            List<int> tagsSelected = new List<int>();
            Post post = _postRepository.GetPublishedPostById(id);

            PostTagFormViewModel vm = new PostTagFormViewModel()
            {
                PostTag = new PostTag(),
                Tag = tags,
                TagsSelected = tagsSelected,
                PostId = id
            };
            if (post.UserProfileId == int.Parse(User.Claims.ElementAt(0).Value))
            {
                return View(vm);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: PostTagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostTagFormViewModel postTagVM)

        {
            try
            {
                foreach (int TagId in postTagVM.TagsSelected)
                {

                    PostTag newPostTag = new PostTag
                    {
                        PostId = postTagVM.PostId,
                        TagId = TagId
                    };
                    _postTagRepository.AddPostTag(newPostTag);
                }
                return RedirectToAction("Details", "Post", new { id = postTagVM.PostId });
            }
            catch
            {

                IEnumerable<Tag> tags = _tagRepository.GetAllTags();

                List<int> tagsSelected = new List<int>();

                PostTagFormViewModel vm = new PostTagFormViewModel()
                {
                    PostTag = new PostTag(),
                    Tag = tags,
                    TagsSelected = tagsSelected,
                    PostId = postTagVM.PostId
                };
                return View(vm);


            }
        }

        // GET: PostTagController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostTagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostTagController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostTagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
