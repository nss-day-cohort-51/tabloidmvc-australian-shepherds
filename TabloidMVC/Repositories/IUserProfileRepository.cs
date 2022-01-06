using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        void AddUser(UserProfile profile);
        List<UserProfile> GetAll();
        UserProfile GetByEmail(string email);
        UserProfile GetUserById(int id);
        UserProfile GetById(int id);
        void UpdateUser(UserProfile userProfile);
        void Remove(int id);
        void DeactivateUser(UserProfile userProfile);
    }
}