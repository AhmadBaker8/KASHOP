using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if(user is null)
            {
                throw new Exception("Invalid email ro password");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if(!isPasswordValid)
            {
                throw new Exception("Invalid email ro password");
            }
            return new UserResponse
            {
                Email = loginRequest.Email,
            };
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest registerRequest)
        {

            var user = new ApplicationUser()
            {
                FullName = registerRequest.FullName,
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                UserName = registerRequest.UserName
            };
            var Result = await _userManager.CreateAsync(user,registerRequest.Password);
            if(Result.Succeeded)
            {
                return new UserResponse()
                {
                    Email = registerRequest.Email,
                };
            }
            else
            {
                throw new Exception($"{Result.Errors}");
            }

        }
    }
}
