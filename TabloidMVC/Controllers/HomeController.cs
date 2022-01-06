using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository _postRepository;

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepository)
        {
            _logger = logger;
            _postRepository = postRepository;
        }

        public IActionResult Index()
        {

            if(GetCurrentUserProfileId() != 0)
            {
                var subbedId = _postRepository.GetSubscribed(GetCurrentUserProfileId());

                var subbedPosts = _postRepository.GetSubscribedBySubscribedId(subbedId);


                return View(subbedPosts);
            }
            else
            {
                return View(null);
            }
            

            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(id == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(id);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
