using System;
using System.Threading.Tasks;
using _1U_ASP.Models;
using _1U_ASP.Models.Result;
using _1U_ASP.Repositorys.Interface;
using _1U_ASP.Service.Interface;
using Microsoft.AspNetCore.Identity;

namespace _1U_ASP.Service.Impl
{
    public class LoginServices: ILoginServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginServices(
          IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            
            var existingUser = await _unitOfWork.UserManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Result = "User with this email address already exists"
                };
            }

            var newUserId = Guid.NewGuid();
            var newUser = new User
            {
                Id = newUserId.ToString(),
                Email = email,
                UserName = email
            };
            
            var createdUser = await _unitOfWork.UserManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Result = "Errors"
                };
            }
            
            return new AuthenticationResult
            {
                Result = "Success"
            };
        }

        public async Task<SignInResult> Login(LoginViewModel model)
        {
            var result =
                await _unitOfWork.SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            return result;
        }




    }
}
