using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAll();
        UserProfile GetByEmail(string email);
        UserProfile GetUserById(int id);
        UserProfile GetById(int id);
    }
}