using _1U_ASP.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.MiddleTier.Interface
{
    public interface ILoginServices
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}
