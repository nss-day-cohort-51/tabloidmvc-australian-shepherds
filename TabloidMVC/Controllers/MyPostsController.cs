using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class MyPostsController : Controller
    {
        public IPostRepository _postRepository;
        public MyPostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        } 
        // GET: MyPostsController
        public ActionResult Index()
        {
            int userId = GetCurrentUserProfileId();

            var userPosts = _postRepository.GetUsersPublishedPostsByUserId(userId);

            return View(userPosts);
        }

        // GET: MyPostsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MyPostsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyPostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: MyPostsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MyPostsController/Edit/5
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

        // GET: MyPostsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MyPostsController/Delete/5
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

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
