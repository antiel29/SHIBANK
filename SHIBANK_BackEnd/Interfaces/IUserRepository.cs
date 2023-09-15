﻿using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUser(string username);
        bool UserExists(int id);
    }
}
