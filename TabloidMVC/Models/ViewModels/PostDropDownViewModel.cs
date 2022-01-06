using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostDropDownViewModel
    {
        public List<Category> CategoriesIds { get; set;}
        public List<UserProfile> UserIds { get; set; }
        public int selectedUser { get; set; }
        public int selectedCategory { get; set; }
        public List<Post> Posts {get; set;}
        public Post Post { get; set; }
    }
}
