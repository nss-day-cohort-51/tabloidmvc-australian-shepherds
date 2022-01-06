using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostDropDownViewModel
    {
        public MultiSelectList CategoriesIds { get; set;}
        public MultiSelectList UserIds { get; set; }
        public List<int> selectedUsers { get; set; }
        public List<int> selectedCategories { get; set; }
        public List<Post> Posts {get; set;}
        public Post Post { get; set; }
    }
}
