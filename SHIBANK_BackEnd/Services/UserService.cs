using Microsoft.AspNetCore.Identity;
using SHIBANK.Dto;
using SHIBANK.Interfaces;
using SHIBANK.Models;

namespace SHIBANK.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        public UserService(IUserRepository userRepository,UserManager<User> userManager) 
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public bool DeleteUser(User user)
        {
            return _userRepository.DeleteUser(user);
        }

        public User? GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public User? GetUser(string username)
        {
            return _userRepository.GetUser(username);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public async Task<bool> RegisterUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user,password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                return true;
            }
            return false;
        }

        public async Task<bool> ChangePassword(User user,string oldPassword,string newPassword)
        {
            if(await _userManager.CheckPasswordAsync(user, oldPassword)) 
            {
                await _userManager.ChangePasswordAsync(user,oldPassword,newPassword);
                return true;
            }
            return false;
        }
        public bool UpdateUser(User user,UserUpdateDto userUpdateDto)
        {
            user.UserName = userUpdateDto.Username ?? user.UserName;
            user.FirstName = userUpdateDto.FirstName ?? user.FirstName;
            user.LastName = userUpdateDto.LastName ?? user.LastName;
            user.Email = userUpdateDto.Email ?? user.Email;

            return _userRepository.UpdateUser(user);
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
