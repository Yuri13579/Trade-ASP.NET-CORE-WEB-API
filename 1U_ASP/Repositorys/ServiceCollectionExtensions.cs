﻿using _1U_ASP.Repositorys.Interface;
using _1U_ASP.Security.Model;
using _1U_ASP.Security.Service;
using _1U_ASP.Service.Impl;
using _1U_ASP.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace _1U_ASP.Repositorys
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMainService(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ISaleOrderSevrice, SaleOrderServices>();
            services.AddTransient<ILoginServices, LoginServices>();
            services.AddTransient<ILoginServices, LoginServices>();
            services.AddTransient<ISaleOrderSevrice, SaleOrderServices>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProviderService, ProviderService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IShopService, ShopService>();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserAccountProcessing, UserAccountProcessing>();
            services.AddScoped<IUserAccountProcessing, UserAccountProcessing>();
            services.AddScoped<ILogActionServeProcess, LogActionProcessing>();
            services.AddTransient<IDocEnterProductService, DocEnterProductService>();
            
            return services;
        }
    }
}
