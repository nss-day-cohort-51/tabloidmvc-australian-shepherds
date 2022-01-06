using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Repositories;
using TabloidMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace TabloidMVC.Controllers
{
    public class UserProfileController : Controller
    {
        public IUserProfileRepository _userProfileRepository;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        // GET: UserProfileController
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var profiles = _userProfileRepository.GetAll();

            return View(profiles);
        }

        // GET: UserProfileController/Details/5
        public ActionResult Details(int id)
        {
            var user = _userProfileRepository.GetUserById(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return NotFound();
            }

        }
        // GET: UserProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfileController/Create
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

        // GET: UserProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _userProfileRepository.GetUserById(id);
            return View(user);
        }

        // POST: UserProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile userProfile)
        {
            try
            {
                userProfile.ImageLocation = null;
                _userProfileRepository.UpdateUser(userProfile);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(userProfile);
            }
        }

        // GET: UserProfileController/Delete/5
        public ActionResult Deactivate(int id)
        {
            var user = _userProfileRepository.GetById(id);
            return View(user);
        }

        // POST: UserProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(int id, IFormCollection collection)
        {
            try
            {
                _userProfileRepository.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }
    }
}
