using System;
using System.Text;
using System.Threading.Tasks;
using _1U_ASP.Context;
using _1U_ASP.Security.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using _1U_ASP.Repositorys;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace _1U_ASP
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            GlobalVariables.EnvironmentType = configuration.GetSection(Const.Startup.DeploymentEnvironment).Value;
            Configuration = new ConfigurationBuilder()
                .SetBasePath(configuration.GetSection(Const.Startup.ContentRoot).Value)
                .AddJsonFile(Const.Startup.AppSettingsJson, false, true)
                .AddJsonFile($"appsettings.{GlobalVariables.EnvironmentType}.json", true, true)
                .AddEnvironmentVariables().Build();
            
            GlobalVariables.ConnectionStringMainDatabase = Configuration.GetConnectionString(Const.Startup.DefaultConnection);
            GlobalVariables.SecretKey =
                Configuration.GetSection(nameof(JwtIssuerOptions))[nameof(JwtIssuerOptions.SecretKey)];
            GlobalVariables.Audience =
                Configuration.GetSection(nameof(JwtIssuerOptions))[nameof(JwtIssuerOptions.Audience)];
            GlobalVariables.Issuer =
                Configuration.GetSection(nameof(JwtIssuerOptions))[nameof(JwtIssuerOptions.Issuer)];
          
            GlobalVariables.SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    Configuration.GetSection(nameof(JwtIssuerOptions))[Const.Startup.SecretKey])),
                SecurityAlgorithms.HmacSha256);
          
            //Configuration = configuration;
            //Configuration.GetConnectionString("DefaultConnection");

            //GlobalVariables.ConnectionStringMainDatabase = Configuration.GetConnectionString("DefaultConnection");
            //GlobalVariables.SecretKey = Configuration.GetSection(nameof(JwtIssuerOptions))[nameof(JwtIssuerOptions.SecretKey)];
        }
            
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
           services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(GlobalVariables.ConnectionStringMainDatabase
                 ));

            services.AddMainService();
            
            services.AddIdentity<AppUser, AppRole>(
                options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                }).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = GlobalVariables.Issuer,
                        ValidAudience = GlobalVariables.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalVariables.SecretKey)),
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                path.StartsWithSegments("/note")
                                || path.StartsWithSegments("/mainchat"))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
            });
                
            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings.
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequiredUniqueChars = 1;

            //    // Lockout settings.
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Lockout.MaxFailedAccessAttempts = 5;
            //    options.Lockout.AllowedForNewUsers = true;

            //    // User settings.
            //    options.User.AllowedUserNameCharacters =
            //        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //    options.User.RequireUniqueEmail = false;
            //});

            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationContext>();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                           // .WithMethods("GET", "POST", "DELETE", "PUT"); // .AllowAnyOrigin();
                    });
            });
            services.AddMemoryCache();
            
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseSwagger();
            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/swagger"), builder =>
            {
                builder.UseMvc();
            });
            app.UseSwaggerUI(c =>
            {c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/swagger"), builder =>
            {
                builder.UseMvc();
            });

            //app.UseMvc();

        }
    }
}
