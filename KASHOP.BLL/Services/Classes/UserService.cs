using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users =  await _userRepository.GetAllAsync();
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    EmailConfirmed = user.EmailConfirmed,
                    RoleName = role.FirstOrDefault() // Assuming a user has only one role
                });
            }

            return userDtos;
        }
        public async Task<UserDto> GetByIdAsync(string userId)
        {
            var user =  await _userRepository.GetByIdAsync(userId);
            return user.Adapt<UserDto>();
        }

        public Task<bool> IsBlockedAsync(string userId)
        {
            return _userRepository.IsBlockedAsync(userId);
        }

        public Task<bool> UnBlockUserAsync(string userId)
        {
            return _userRepository.UnBlockUserAsync(userId);
        }
        public Task<bool> BlockUserAsync(string userId, int days)
        {
            return _userRepository.BlockUserAsync(userId, days);
        }

        public async Task<bool> ChangeUserRoleAsync(string userId, string newRole)
        {
            return await _userRepository.ChangeUserRoleAsync(userId, newRole);
        }
    }
}
