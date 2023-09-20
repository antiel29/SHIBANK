using SHIBANK.Dto;
using SHIBANK.Interfaces;
using SHIBANK.Models;
using SHIBANK.Repository;

namespace SHIBANK.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public User GetUser(string username)
        {
            return _userRepository.GetUser(username);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public bool RegisterUser(User user)
        {
            return _userRepository.RegisterUser(user);
        }

        public bool UserExists(int id)
        {
            return _userRepository.UserExists(id);
        }

        public bool UserExists(string username)
        {
            return _userRepository.UserExists(username);
        }
    }
}
