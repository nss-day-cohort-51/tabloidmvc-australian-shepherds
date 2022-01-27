using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TabloidMVC.Repositories;
using TabloidMVC.Models;
using Microsoft.VisualBasic;
using TabloidMVC.Models.ViewModels;

namespace TabloidMVC.Controllers
{
        [Authorize]
        public class CommentController : Controller
        {
            private readonly ICommentRepository _commentRepository;
            private readonly IPostRepository _postRepository;
            private readonly IUserProfileRepository _userProfileRepository;

            public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, IUserProfileRepository userProfileRepository)
            {
                _commentRepository = commentRepository;
                _postRepository = postRepository;
                _userProfileRepository = userProfileRepository;

            }
            public IActionResult Index()
        {
            return View();
        }

        public IActionResult DeleteComment(int id)
        {
            var post = _commentRepository.GetSingleComment(id);

            _commentRepository.Remove(id);
            return RedirectToAction("CommentList", "Comment", new { id = post.PostId });
        }

        public IActionResult Edit(int id)
        {
            var comment = _commentRepository.GetSingleComment(id);
            return View(comment);
              
        }

        [HttpPost]
        public IActionResult Edit(Comment comment)
        {
            try
            {
                _commentRepository.UpdateComment(comment);

                return RedirectToAction("CommentList", "Comment", new { id = comment.PostId });
            }
            catch(Exception ex)
            {
                return View(comment);
            }
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

        public IActionResult CommentList(int id)
        {
            var postCommentList = _commentRepository.GetCommentsByPostId(id);
            var post = _postRepository.GetPublishedPostById(id);
            var user = _userProfileRepository.GetUserById(GetCurrentUserProfileId());
            var viewCommentList = new CommentListView
            {
                CommentList = postCommentList,
                Post = post,
                User = user            
            };

            return View(viewCommentList);
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}