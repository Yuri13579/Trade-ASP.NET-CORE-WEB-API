using System.Threading.Tasks;
using _1U_ASP.Models;
using _1U_ASP.Models.Result;
using Microsoft.AspNetCore.Identity;

namespace _1U_ASP.Service.Interface
{
    public interface ILoginServices
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        Task<SignInResult> Login(LoginViewModel model);
    }
}
