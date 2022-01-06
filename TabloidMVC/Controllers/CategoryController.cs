using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Repositories;
using TabloidMVC.Models;
using System.Security.Claims;
using System;

namespace TabloidMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository, IUserProfileRepository userProfileRepository)
        {
            _categoryRepository = categoryRepository;
            _userProfileRepository = userProfileRepository;
        }
        // GET: CategoryController
        public IActionResult Index()
        {
            var catagories = _categoryRepository.GetAll();
            return View(catagories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = int.Parse(currentUserId);
            UserProfile user = _userProfileRepository.GetById(id);

            if (user.UserTypeId == 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                _categoryRepository.AddNew(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = _categoryRepository.GetById(id);
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            try
            {
                _categoryRepository.Update(category);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(category);
            }


        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
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
            var categories = _categoryRepository.GetAll();
            return View(categories);
        }
    }
}
