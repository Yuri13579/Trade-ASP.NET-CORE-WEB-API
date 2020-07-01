using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Const;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using _1U_ASP.Repositorys.Interface;
using _1U_ASP.Security.Model;
using Microsoft.AspNetCore.Identity;

namespace _1U_ASP.Security.Service
{
    public class AccountService : IAccountService
    {
        #region DI
        private readonly IUserAccountProcessing _userAccountProcessing;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Profile> _profile;
        
        public AccountService(
            IUserAccountProcessing userAccountProcessing,
            IPasswordHasher<AppUser> passwordHasher,
            UserManager<AppUser> userManager,
            IRepository<Profile> profile
            )
        {
            _userAccountProcessing = userAccountProcessing;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
            _profile = profile;
        }
        #endregion


        public async Task<DataServiceMessage> Register(RegisterViewModel model)
        {
            var appUser = await _userAccountProcessing.GetFullUserByEmail(model.Email);
            if (appUser != null)
                return new DataServiceMessage
                {
                    Result = false,
                    MainMessage = BadResponses.UserAlreadyExists
                };
          
            //appUser = await _userAccountProcessing.AddJobSeekerUserInRegister(
            //    model.Email,
            //    model.Password,
            //    new AppPerson
            //    {
            //        FirstName = model.FirstName,
            //        LastName = model.LastName,
            //        EmailAddress = model.Email,
            //        DisplayName = $"{model.FirstName} {model.LastName}",
            //    });

            appUser = await _userAccountProcessing.AddCompanyOwnerUserInRegister(
                model.Email,
                model.Password,
                SysConst.MainCompanyId,
                SysConst.MainCompanyName,
                new AppPerson
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailAddress = model.Email,
                    DisplayName = $"{model.FirstName} {model.LastName}",
                    MobilePhone = ""
                    //UserActionId = userActionId
                });

            await AddProfile(
                appUser.PersonId.GetValueOrDefault(),
                model);
            return new DataServiceMessage
            {
                Result = true,
                MainMessage = GoodResponses.AddedSuccessfully
            };
           
        }

        private async Task AddProfile(
            int personId,
            RegisterViewModel model)
        {
            if (string.IsNullOrEmpty(model.MyRole))
                model.MyRole = SystemMessage.Untitled;

            var profile = await _profile.AddAsync(new Profile
            {
                PersonId = personId,
                ProfileTypeSysCodeUniqueId = SysCodeUniqueId.ProfileTypeEmployee,
                ProfileTitle = model.MyRole,
                Status = true,
                Deleted = false
            });
            
        }
       
        public async Task<DataServiceMessage> Login(string remoteIpAddress, LoginViewModel model)
        {
            var user = await _userAccountProcessing.GetFullUserByEmail(model.Email);

            if (user == null || user.Person.Deleted == true)
                return new DataServiceMessage
                {
                    Result = false,
                    MainMessage = BadResponses.UserIsnTFound
                };
            
            //if (await _userManager.IsLockedOutAsync(user))
            //    return  BadResponses.YouCanTryAgainInMinutes;

            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password)
                != PasswordVerificationResult.Success)
                return new DataServiceMessage
                {
                    Result = false,
                    MainMessage = BadResponses.PasswordIsnTValid
                };
           
            if (string.IsNullOrEmpty(model.ReturnUrl))
                model.ReturnUrl = "";


            if (user.AccessFailedCount > 0)
                await _userManager.ResetAccessFailedCountAsync(user);

            bool resumeIsNotFilled = false;

            if (user.Claims.Count > 0)
                resumeIsNotFilled = user.Claims.FirstOrDefault(x => x.ClaimType == SystemMessage.ResumeIsNotFilled) != null;

            //if (!string.IsNullOrEmpty(model.AccessToken) && !string.IsNullOrEmpty(model.ExternalProviderName))
            //{
            //    var attachProviderResult = await _loginProvider.AttachExternalLoginAsync(new AttachModel
            //    {
            //        AccessToken = model.AccessToken,
            //        ExternalProviderName = model.ExternalProviderName,
            //        User = user
            //    });

            //    if (!attachProviderResult.Result)
            //        return new DataServiceMessage
            //        {
            //            Result = false,
            //            MainMessage = BadResponses.ExternalLoginAttachingOperationFailed
            //        };
            //}

            var token = new JwtSecurityTokenHandler().WriteToken(
                TokenProcessing.GetJwtSecurityToken(
                    user,
                    ""));
            return new DataServiceMessage
            {
                Result = true,
                MainMessage = token
            };
        }

        public Task<bool> CheckEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> EmployerRegister(RegisterViewModel model, string remoteIpAddress, int userActionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PersonAccountLevel>> GetAccountInfo(JwtSecurityToken jwtSecurityToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCustomerToken(int employerId, string remoteIpAddress, JwtSecurityToken tokenJwt)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetExternalProviders()
        {
            throw new NotImplementedException();
        }

        public string ProlongToken(string remoteIpAddress, JwtSecurityToken jwtSecurityToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> RefreshToken(RefreshToken refreshToken, string remoteIpAddress, JwtSecurityToken jwtSecurityToken)
        {
            throw new NotImplementedException();
        }
        
        public Task<string> SignOut(SignOutModel model, JwtSecurityToken tokenJwt, int userActionId)
        {
            throw new NotImplementedException();
        }
    }
}
