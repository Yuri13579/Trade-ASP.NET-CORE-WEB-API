using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using _1U_ASP.Security.Model;

namespace _1U_ASP.Security.Service
{
    public interface IAccountService
    {
        Task<bool> CheckEmail(
            string email);
        
        Task<List<string>> GetExternalProviders();

        //Task<List<ExternalProviderInfo>> GetExternalProvidersInfo(
        //    string email);

        Task<List<PersonAccountLevel>> GetAccountInfo(
            JwtSecurityToken jwtSecurityToken);

        Task<string> GetCustomerToken(
            int employerId,
            string remoteIpAddress,
            JwtSecurityToken tokenJwt);
        
        Task<DataServiceMessage> Login(
            string remoteIpAddress,
           LoginViewModel model);

        Task<DataServiceMessage> Register(
            RegisterViewModel model
            );
        
        //Task<string> ChangeUserEmail(
        //    ResetEmailModel model,
        //    JwtSecurityToken jwtSecurityToken,
        //    string remoteIpAddress,
        //    int userActionId);
        
        Task<string> RefreshToken(
            RefreshToken refreshToken,
            string remoteIpAddress,
            JwtSecurityToken jwtSecurityToken);

        string ProlongToken(
            string remoteIpAddress,
            JwtSecurityToken jwtSecurityToken);
        
        //Task<DataServiceMessage> ForgotPassword(
        //    ForgotPassword model,
        //    string remoteIpAddress);

        //Task<DataServiceMessage> ResetPassword(
        //    ResetPasswordViewModel model,
        //    string email,
        //    JwtSecurityToken jwtSecurityToken);

        Task<string> EmployerRegister(
           RegisterViewModel model,
           string remoteIpAddress,
           int userActionId);

        Task<string> SignOut(
            SignOutModel model,
            JwtSecurityToken tokenJwt,
            int userActionId);

        //Task<DataServiceMessage> DeleteUser(
        //    SignOutModel model,
        //    JwtSecurityToken tokenJwt,
        //    int userActionId);
    }
}
