using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostTagRepository
    {
        void AddPostTag(PostTag postTag);
        List<PostTag> GetAllPostTagsByPostId(int id);
        PostTag GetById(int id);
    }
}
