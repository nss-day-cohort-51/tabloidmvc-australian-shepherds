using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class CommentListView
    {
        public Comment Comment { get; set; }
        public List<Comment> CommentList {get; set;}
        public Post Post { get; set; }
        public UserProfile User { get; set; }
    }
}
