using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        // GET: TagController
        public ActionResult Index()
        {
            var tags = _tagRepository.GetAllTags();
            return View(tags);
        }

        // GET: TagController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TagController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            try
            {
                _tagRepository.AddTag(tag);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(tag);
            }
        }

        // GET: TagController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TagController/Edit/5
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

        // GET: TagController/Delete/5
        public ActionResult Delete(int id)
        {
            _tagRepository.Remove(id);
            return RedirectToAction("Index");
        }

        // POST: TagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
