﻿using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        void AddUser(UserProfile profile);
        List<UserProfile> GetAll();
        UserProfile GetByEmail(string email);
    }
}