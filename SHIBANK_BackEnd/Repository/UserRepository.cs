﻿using SHIBANK.Data;
using SHIBANK.Interfaces;
using SHIBANK.Models;

namespace SHIBANK.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) 
        {
            _context = context;
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(u=>u.Id == id).FirstOrDefault();
        }

        public User GetUser(string username)
        {
            return _context.Users.Where(u => u.Username == username).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u=>u.Id == id);
        }
        public bool UserExists(string username) 
        {
            return _context.Users.Any(u=>u.Username == username);
        }
    }
}
