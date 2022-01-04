using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
        [Authorize]
        public class CommentController : Controller
        {
            private readonly ICommentRepository _commentRepository;

            public CommentController(ICommentRepository commentRepository)
            {
                _commentRepository = commentRepository;
            }
            public IActionResult Index()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            _commentRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}