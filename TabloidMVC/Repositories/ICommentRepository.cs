using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetAll();
        List<Comment> GetCommentsByPostId(int id);
        void Remove(int id);
        void Add(Comment comment);
        Comment GetSingleComment(int id);
    }
}