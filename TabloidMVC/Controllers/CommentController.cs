﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TabloidMVC.Repositories;
using TabloidMVC.Models;
using Microsoft.VisualBasic;

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
        public IActionResult Create(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(int id, Comment comment)
        {
            try
            {
                comment.CreateDateTime = DateAndTime.Now;
                comment.UserProfileId = GetCurrentUserProfileId();
                comment.PostId = id;
                _commentRepository.Add(comment);
                return RedirectToAction("Details", "Post", new {id = id });
            }
            catch (Exception ex)
            {
                return View(comment);
            }

        }
        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}