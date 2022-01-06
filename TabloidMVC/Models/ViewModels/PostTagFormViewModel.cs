using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostTagFormViewModel
    {
        public PostTag PostTag { get; set; }
        public int PostId { get; set; }
        public IEnumerable<PostTag> PostTags { get; set; }
        public IEnumerable<Tag> Tag { get; set; }
        public List<int> PostTagsSelected { get; set; }
        public List<int> TagsSelected { get; set; }
    }
}
