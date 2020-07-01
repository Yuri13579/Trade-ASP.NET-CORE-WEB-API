using Microsoft.AspNetCore.Identity;
using System;
using _1U_ASP.Models;

namespace _1U_ASP.Repositorys.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        UserManager<User> UserManager { get; }
        SignInManager<User> SignInManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
    }
}
