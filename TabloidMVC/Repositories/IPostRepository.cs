using System;
using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        void Remove(int id);
        List<Post> GetAllPublishedPosts();
        Post GetPublishedPostById(int id);
        Post GetUserPostById(int id, int userProfileId);
        void UpdatePost(Post post);
        List<Post> GetUsersPublishedPostsByUserId(int id);
        void Subscribe(int userId, int currentUserId, DateTime beginDateTime);
        int GetAuthorIdByPostId(int postId);
        List<Post> GetSubscribedBySubscribedId(int id);
        int GetSubscribed(int currentUserId);
    }
}