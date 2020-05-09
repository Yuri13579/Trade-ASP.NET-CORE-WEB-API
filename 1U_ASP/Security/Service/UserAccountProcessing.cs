using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using _1U_ASP.Const;
using _1U_ASP.Models;
using _1U_ASP.Repositorys.Interface;
using _1U_ASP.Security.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _1U_ASP.Security.Service
{
    public class UserAccountProcessing : IUserAccountProcessing
    {
        #region DI
        //private readonly IRepository<EmployerCompany> _employerCompany;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IRepository<Person> _person;

        public UserAccountProcessing(
           // IRepository<EmployerCompany> employerCompany,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IRepository<Person> person)
        {
           // _employerCompany = employerCompany;
            _userManager = userManager;
            _roleManager = roleManager;
            _person = person;
        }
        #endregion

        public async Task<AppUser> GetFullUserByEmail(
            string email)
        {
            var user = await _userManager.Users
                        .Where(x => x.Email == email)
                        .Include(e => e.Claims)
                        .ThenInclude(m => m.UserClaimRoles)
                        .Include(y => y.UserRoles)
                        .ThenInclude(ur => ur.Role)
                        .Include(k => k.Person)
                        .FirstOrDefaultAsync();

            if (user == null)
                return null;

            var userClaims = user.Claims.Where(z => z.UserClaimRoles.Count() > 0).ToList();

            foreach (var i in userClaims)
            {
                foreach (var j in i.UserClaimRoles)
                {
                    if (j.Role == null)
                    {
                        var role = await _roleManager.FindByIdAsync(j.RoleId);
                        j.Role = role;
                    }
                }
            }
            return user;
        }

        public async Task<string> GetRoleIdByName(
            string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            return role.Id;
        }

        public async Task<AppUser> GetFullUserById(
            string id)
        {
            var user = await _userManager.Users
                        .Where(x => x.Id == id)
                        .Include(e => e.Claims)
                        .ThenInclude(m => m.UserClaimRoles)
                        .Include(y => y.UserRoles)
                        .ThenInclude(ur => ur.Role)
                        .Include(k => k.Person)
                        .FirstOrDefaultAsync();

            if (user == null)
                return null;

            var userClaims = user.Claims.Where(z => z.UserClaimRoles.Count() > 0).ToList();

            foreach (var i in userClaims)
            {
                foreach (var j in i.UserClaimRoles)
                {
                    if (j.Role == null)
                    {
                        var role = await _roleManager.FindByIdAsync(j.RoleId);
                        j.Role = role;
                    }
                }
            }
            return user;
        }

        public async Task<AppUser> GetFullUserByPersonId(
            int id)
        {
            var user = await _userManager.Users
                        .Where(x => x.PersonId == id)
                        .Include(e => e.Claims)
                        .ThenInclude(m => m.UserClaimRoles)
                        .Include(y => y.UserRoles)
                        .ThenInclude(ur => ur.Role)
                        .Include(k => k.Person)
                        .FirstOrDefaultAsync();

            if (user == null)
                return null;

            var userClaims = user.Claims.Where(z => z.UserClaimRoles.Count() > 0).ToList();

            foreach (var i in userClaims)
            {
                foreach (var j in i.UserClaimRoles)
                {
                    if (j.Role == null)
                    {
                        var role = await _roleManager.FindByIdAsync(j.RoleId);
                        j.Role = role;
                    }
                }
            }
            return user;
        }

        public async Task<AppUser> GetFullUserByJwtSecurityToken(
            JwtSecurityToken tokenJwt)
        {
            var userId = GlobalMethods.GetUserIdFromJwtSecurityToken(tokenJwt);

            return await GetFullUserById(userId);
        }

        public async Task<AppUser> GetFullUserByLoginProvider(
            string loginProvider,
            string providerKey)
        {
            return await _userManager.Users
             .Where(x => x.Logins.Where(d => d.LoginProvider == loginProvider
             && d.ProviderKey == providerKey).FirstOrDefault() != null)
             .Include(e => e.Claims)
             .ThenInclude(m => m.UserClaimRoles)
             .Include(y => y.UserRoles)
             .ThenInclude(ur => ur.Role)
             .Include(z => z.Person)
             .FirstOrDefaultAsync();
        }

        public async Task<IdentityResult> AddLoginProvider(
            AppUser appUser,
            UserLoginInfo userLoginInfo)
        {
            return await _userManager.AddLoginAsync(appUser, userLoginInfo);
        }

        public async Task<IdentityResult> AddToRole(
            AppUser appUser,
            string roleName)
        {
            return await _userManager.AddToRoleAsync(appUser, roleName);
        }

        public async Task<IdentityResult> RemoveFromRole(
            AppUser appUser,
            string roleName)
        {
            return await _userManager.RemoveFromRoleAsync(appUser, roleName);
        }

        public async Task<bool> IsInRole(AppUser appUser, string roleName)
        {
            return await _userManager.IsInRoleAsync(appUser, roleName);
        }

        public async Task<AppUser> AddUser(
            string email,
            int personId)
        {
            var appUser = new AppUser
            {
                Email = email,
                UserName = email,
                PersonId = personId
            };

            await _userManager.CreateAsync(appUser, email.Substring(0, email.IndexOf('@')) + SystemMessage.UnderscoreTemp);
            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            await _userManager.ConfirmEmailAsync(appUser, confirmEmailToken);
            appUser = await GetFullUserById(appUser.Id);
            return appUser;
        }

        /// <summary>
        /// adds role and claims for jobseeker
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public async Task AddJobSeekerAccount(
            AppUser appUser)
        {
            var accountSchemaClaim = await GetAccountSchema(appUser);
            if (accountSchemaClaim == null)
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimAccountSchema,
                    ClaimValue = ((int)Authorize.AccountLevel.jobseeker).ToString()
                });
            else
            {
                var accountSchema = (Authorize.AccountLevel)Convert.ToInt32(accountSchemaClaim.ClaimValue);
                if (!((accountSchema & Authorize.AccountLevel.jobseeker) == Authorize.AccountLevel.jobseeker))
                {
                    appUser.Claims.Remove(accountSchemaClaim);
                    accountSchema |= Authorize.AccountLevel.jobseeker;
                    appUser.Claims.Add(new AppUserClaim
                    {
                        UserId = appUser.Id,
                        ClaimType = Authorize.Claims.ClaimAccountSchema,
                        ClaimValue = ((int)accountSchema).ToString()
                    });
                }
            }

            var defaultAccountSchema = await GetDefaultAccountSchema(appUser);
            if (defaultAccountSchema == null)
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimDefaultSchema,
                    ClaimValue = ((int)Authorize.AccountLevel.jobseeker).ToString()
                });

            var jobseekerRoleClaim = appUser.UserRoles.Where(x => x.Role.Name == Authorize.Roles.JobSeeker).FirstOrDefault();
            if (jobseekerRoleClaim == null)
            {
                var jobseekerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.JobSeeker).First();
                appUser.UserRoles.Add(new AppUserRole
                {
                    UserId = appUser.Id,
                    RoleId = jobseekerRole.Id
                });
            }
            await _userManager.UpdateAsync(appUser);
        }

        /// <summary>
        /// adds roles and claims for employee
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="employerId"></param>
        /// <returns></returns>
        public async Task AddEmployeeClaims(
            AppUser appUser,
            string employerName,
            int employerId)
        {
            var accountSchemaClaim = await GetAccountSchema(appUser);
            if (accountSchemaClaim == null)
            {
                await AddJobSeekerAccount(appUser);
                appUser = await GetFullUserById(appUser.Id);
                accountSchemaClaim = await GetAccountSchema(appUser);
            }
            var accountSchema = (Authorize.AccountLevel)Convert.ToInt32(accountSchemaClaim.ClaimValue);
            if (!((accountSchema & Authorize.AccountLevel.employee) == Authorize.AccountLevel.employee))
            {
                appUser.Claims.Remove(accountSchemaClaim);
                accountSchema |= Authorize.AccountLevel.employee;
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimAccountSchema,
                    ClaimValue = ((int)accountSchema).ToString()
                });
            }

            var defaultAccountSchema = await GetDefaultAccountSchema(appUser);
            if (defaultAccountSchema == null)
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimDefaultSchema,
                    ClaimValue = ((int)Authorize.AccountLevel.employee).ToString()
                });

            var employeeRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.Employee).First();
            var employeeRoleClaim = appUser.UserRoles.Where(x => x.Role.Name == Authorize.Roles.Employee);
            if (employeeRoleClaim == null)
            {

                appUser.UserRoles.Add(new AppUserRole
                {
                    UserId = appUser.Id,
                    RoleId = employeeRole.Id
                });
            }

            var employerIdClaim = await GetEmployerIdClaim(
                appUser,
                employerId);

            if (employerIdClaim == null)
            {
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimEmployerId,
                    ClaimValue = employerId.ToString(),
                    ClaimProperty = employerName
                });
                await _userManager.UpdateAsync(appUser);
                appUser = await GetFullUserById(appUser.Id);
                employerIdClaim = await GetEmployerIdClaim(
                appUser,
                employerId);
            }

            var defaultEmployerIdClaim = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultEmployerId).FirstOrDefault();
            if (defaultEmployerIdClaim == null)
            {
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimDefaultEmployerId,
                    ClaimValue = employerId.ToString(),
                    ClaimProperty = employerName
                });
            }

            var claimRoleEmployee = employerIdClaim.UserClaimRoles.Where(x => x.RoleId == employeeRole.Id).FirstOrDefault();
            if (claimRoleEmployee == null)
                AddRoleClaimToClaim(employerIdClaim, employeeRole.Id);

            await _userManager.UpdateAsync(appUser);
        }

        #region Company
        /// <summary>
        /// sets company claims for manager
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="accountId"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public async Task AddCompanyManagerClaims(
            AppUser appUser,
            int accountId,
            string companyName)
        {
            var accountIdUserClaim = await GetAccountIdClaim(appUser, accountId);
            if (accountIdUserClaim == null)
            {
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimAccountId,
                    ClaimValue = accountId.ToString(),
                    ClaimProperty = companyName
                });
            }

            var defaultAccountIdClaim = await GetDefaultAccountIdClaim(appUser);
            if (defaultAccountIdClaim == null)
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimDefaultAccountId,
                    ClaimValue = accountId.ToString(),
                    ClaimProperty = companyName
                });

            var accountSchemaClaim = await GetAccountSchema(appUser);
            if (accountSchemaClaim == null)
            {
                await AddJobSeekerAccount(appUser);
                appUser = await GetFullUserById(appUser.Id);
                accountSchemaClaim = await GetAccountSchema(appUser);
            }
            var accountSchema = (Authorize.AccountLevel)Convert.ToInt32(accountSchemaClaim.ClaimValue);

            if (!((accountSchema & Authorize.AccountLevel.company) == Authorize.AccountLevel.company))
            {
                appUser.Claims.Remove(accountSchemaClaim);
                accountSchema |= Authorize.AccountLevel.company;
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimAccountSchema,
                    ClaimValue = ((int)accountSchema).ToString()
                });
            }

            var defaultAccountSchema = await GetDefaultAccountSchema(appUser);
            if (defaultAccountSchema == null)
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimDefaultSchema,
                    ClaimValue = ((int)Authorize.AccountLevel.company).ToString()
                });
            await _userManager.UpdateAsync(appUser);
        }

        /// <summary>
        /// modified company manager roles by condition 
        /// </summary>
        /// <param name="profilePosition"></param>
        /// <param name="accountId"></param>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public async Task RoleUpdateCompanyManager(
            string profilePosition,
            int accountId,
            AppUser appUser)
        {
            var accountIdUserClaim = await GetAccountIdClaim(
                appUser,
                accountId);

            var companyOwnerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CompanyOwner).First();
            var companyAdminRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CompanyAdmin).First();
            var companyManagerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CompanyManager).First();

            switch (profilePosition)
            {
                case SystemMessage.CompanyOwner:
                    await AddRoleToUser(appUser, companyOwnerRole.Name);
                    await AddRoleToUser(appUser, companyAdminRole.Name);
                    await AddRoleToUser(appUser, companyManagerRole.Name);

                    AddRoleClaimToClaim(accountIdUserClaim, companyOwnerRole.Id);
                    AddRoleClaimToClaim(accountIdUserClaim, companyAdminRole.Id);
                    AddRoleClaimToClaim(accountIdUserClaim, companyManagerRole.Id);

                    break;

                case SystemMessage.CompanyExecutive:
                    await RemoveRoleFromUser(appUser, companyOwnerRole.Name);
                    await AddRoleToUser(appUser, companyAdminRole.Name);
                    await AddRoleToUser(appUser, companyManagerRole.Name);

                    RemoveRoleClaimFromClaim(accountIdUserClaim, companyOwnerRole.Id);
                    AddRoleClaimToClaim(accountIdUserClaim, companyAdminRole.Id);
                    AddRoleClaimToClaim(accountIdUserClaim, companyManagerRole.Id);

                    break;

                case SystemMessage.CompanyAssistant:
                    await RemoveRoleFromUser(appUser, companyOwnerRole.Name);
                    await RemoveRoleFromUser(appUser, companyAdminRole.Name);
                    await AddRoleToUser(appUser, companyManagerRole.Name);

                    RemoveRoleClaimFromClaim(accountIdUserClaim, companyOwnerRole.Id);
                    RemoveRoleClaimFromClaim(accountIdUserClaim, companyAdminRole.Id);
                    AddRoleClaimToClaim(accountIdUserClaim, companyManagerRole.Id);

                    break;
            }
            await _userManager.UpdateAsync(appUser);
        }

        /// <summary>
        /// removed company roles and claims
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task DeleteCompanyManagerRolesAndClaims(
            AppUser appUser,
            int accountId)
        {
            var rolesToRemove = new List<string> { Authorize.Roles.CompanyOwner, Authorize.Roles.CompanyAdmin, Authorize.Roles.CompanyManager };

            var accountIdUserClaim = await GetAccountIdClaim(
                appUser,
                accountId);

            var companyOwnerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CompanyOwner).First();
            var companyAdminRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CompanyAdmin).First();
            var companyManagerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CompanyManager).First();

            var otherAccountIdClaims = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimAccountId && x.ClaimValue != accountId.ToString()).ToList();

            if (otherAccountIdClaims.Count > 0)
            {
                foreach (var accountClaim in otherAccountIdClaims)
                {
                    var userClaimRole = accountClaim.UserClaimRoles.Where(x => x.RoleId == companyOwnerRole.Id).FirstOrDefault();
                    if (userClaimRole != null)
                        rolesToRemove.Remove(userClaimRole.Role.Name);
                    var userClaimRole1 = accountClaim.UserClaimRoles.Where(x => x.RoleId == companyAdminRole.Id).FirstOrDefault();
                    if (userClaimRole1 != null)
                        rolesToRemove.Remove(userClaimRole1.Role.Name);
                    var userClaimRole2 = accountClaim.UserClaimRoles.Where(x => x.RoleId == companyManagerRole.Id).FirstOrDefault();
                    if (userClaimRole2 != null)
                        rolesToRemove.Remove(userClaimRole2.Role.Name);
                }
            }

            if (rolesToRemove.Count > 0)
            {
                foreach (var roleToRemove in rolesToRemove)
                {
                    if (appUser.UserRoles.Where(x => x.Role.Name == roleToRemove).FirstOrDefault() != null)
                        await _userManager.RemoveFromRoleAsync(appUser, roleToRemove);
                }
            }

            appUser = await GetFullUserById(appUser.Id);

            var accountUserClaimCompanyOwnerRole = accountIdUserClaim.UserClaimRoles.Where(x => x.RoleId == companyOwnerRole.Id).FirstOrDefault();
            if (accountUserClaimCompanyOwnerRole != null)
                accountIdUserClaim.UserClaimRoles.Remove(accountUserClaimCompanyOwnerRole);
            var accountUserClaimCompanyAdminRole = accountIdUserClaim.UserClaimRoles.Where(x => x.RoleId == companyAdminRole.Id).FirstOrDefault();
            if (accountUserClaimCompanyAdminRole != null)
                accountIdUserClaim.UserClaimRoles.Remove(accountUserClaimCompanyAdminRole);
            var accountUserClaimCompanyManagerRole = accountIdUserClaim.UserClaimRoles.Where(x => x.RoleId == companyManagerRole.Id).FirstOrDefault();
            if (accountUserClaimCompanyManagerRole != null)
                accountIdUserClaim.UserClaimRoles.Remove(accountUserClaimCompanyManagerRole);

            appUser.Claims.Remove(accountIdUserClaim);

            await _userManager.UpdateAsync(appUser);
            appUser = await GetFullUserById(appUser.Id);

            if (otherAccountIdClaims.Count < 1)
            {
                var accountSchemaClaim = await GetAccountSchema(appUser);
                appUser.Claims.Remove(accountSchemaClaim);
                var accountSchema = (Authorize.AccountLevel)Convert.ToInt32(accountSchemaClaim.ClaimValue);
                accountSchema ^= Authorize.AccountLevel.company;
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimAccountSchema,
                    ClaimValue = ((int)accountSchema).ToString()
                });
            }

            var defaultSchemaClaim = await GetDefaultAccountSchema(appUser);
            var defaultSchema = (Authorize.AccountLevel)Convert.ToInt32(defaultSchemaClaim.ClaimValue);
            if ((defaultSchema & Authorize.AccountLevel.company) == Authorize.AccountLevel.company)
            {
                appUser.Claims.Remove(defaultSchemaClaim);
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimDefaultSchema,
                    ClaimValue = ((int)Authorize.AccountLevel.jobseeker).ToString()
                });
            }

            var defaultAccountIdClaim = await GetDefaultAccountIdClaim(appUser);
            if (defaultAccountIdClaim != null)
            {
                if (defaultAccountIdClaim.ClaimValue == accountId.ToString())
                {
                    appUser.Claims.Remove(defaultAccountIdClaim);

                    if (otherAccountIdClaims.Count > 0)
                    {
                        appUser.Claims.Add(new AppUserClaim
                        {
                            UserId = appUser.Id,
                            ClaimType = Authorize.Claims.ClaimDefaultAccountId,
                            ClaimValue = otherAccountIdClaims.First().ClaimValue,
                            ClaimProperty = otherAccountIdClaims.First().ClaimProperty
                        });
                    }
                }
            }
            await _userManager.UpdateAsync(appUser);
        }

        /// <summary>
        /// activated company claims
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="accountId"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public async Task ActivateClaimsCompanyManager(
            AppUser appUser,
            int accountId,
            string companyName)
        {
            var accountIdClaim = await GetAccountIdClaim(
                appUser,
                accountId);
            if (accountIdClaim == null)
            {
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimAccountId,
                    ClaimValue = accountId.ToString(),
                    ClaimProperty = companyName
                });
            }

            var defaultAccountIdClaim = await GetDefaultAccountIdClaim(appUser);
            if (defaultAccountIdClaim == null)
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimDefaultAccountId,
                    ClaimValue = accountId.ToString(),
                    ClaimProperty = companyName
                });

            var accountSchemaClaim = await GetAccountSchema(appUser);
            var accountSchema = (Authorize.AccountLevel)Convert.ToInt32(accountSchemaClaim.ClaimValue);

            if (!((accountSchema & Authorize.AccountLevel.company) == Authorize.AccountLevel.company))
            {
                appUser.Claims.Remove(accountSchemaClaim);
                accountSchema |= Authorize.AccountLevel.company;
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimAccountSchema,
                    ClaimValue = ((int)accountSchema).ToString()
                });
            }

            await _userManager.UpdateAsync(appUser);
        }
        #endregion

        #region Customer
        /// <summary>
        /// sets customer claims for managerr
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="employerName"></param>
        /// <param name="employerId"></param>
        /// <returns></returns>
        public async Task AddEmployerManagerClaims(
            AppUser appUser,
            string employerName,
            int employerId)
        {
            var employerIdUserClaim = await GetEmployerIdClaim(appUser, employerId);
            if (employerIdUserClaim == null)
            {
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimEmployerId,
                    ClaimValue = employerId.ToString(),
                    ClaimProperty = employerName
                });
            }

            var defaultEmployerIdClaim = await GetDefaultEmployerIdClaim(appUser);
            if (defaultEmployerIdClaim == null)
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimDefaultEmployerId,
                    ClaimValue = employerId.ToString(),
                    ClaimProperty = employerName
                });

            var accountSchemaClaim = await GetAccountSchema(appUser);
            if (accountSchemaClaim == null)
            {
                await AddJobSeekerAccount(appUser);
                appUser = await GetFullUserById(appUser.Id);
                accountSchemaClaim = await GetAccountSchema(appUser);
            }

            var accountSchema = (Authorize.AccountLevel)Convert.ToInt32(accountSchemaClaim.ClaimValue);
            if (!((accountSchema & Authorize.AccountLevel.customer) == Authorize.AccountLevel.customer))
            {
                appUser.Claims.Remove(accountSchemaClaim);
                accountSchema |= Authorize.AccountLevel.customer;
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimAccountSchema,
                    ClaimValue = ((int)accountSchema).ToString()
                });
            }

            var defaultAccountSchema = await GetDefaultAccountSchema(appUser);
            if (defaultAccountSchema == null)
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimDefaultSchema,
                    ClaimValue = ((int)Authorize.AccountLevel.customer).ToString()
                });
            await _userManager.UpdateAsync(appUser);
        }

        /// <summary>
        /// modified customer manager roles by condition 
        /// </summary>
        /// <param name="adminLevel"></param>
        /// <param name="employerId"></param>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public async Task RoleUpdateEmployerManager(
            string adminLevel,
            int employerId,
            AppUser appUser)
        {
            var employerIdUserClaim = await GetEmployerIdClaim(
                appUser,
                employerId);

            var customerOwnerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CustomerOwner).First();
            var customerAdminRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CustomerAdmin).First();
            var customerAdminManagerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CustomerAdminManager).First();
            var customerManagerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CustomerManager).First();

            switch (adminLevel)
            {
                case SystemMessage.CustomerOwner:
                    await AddRoleToUser(appUser, customerOwnerRole.Name);
                    await AddRoleToUser(appUser, customerAdminRole.Name);
                    await AddRoleToUser(appUser, customerAdminManagerRole.Name);
                    await AddRoleToUser(appUser, customerManagerRole.Name);

                    AddRoleClaimToClaim(employerIdUserClaim, customerOwnerRole.Id);
                    AddRoleClaimToClaim(employerIdUserClaim, customerAdminRole.Id);
                    AddRoleClaimToClaim(employerIdUserClaim, customerAdminManagerRole.Id);
                    AddRoleClaimToClaim(employerIdUserClaim, customerManagerRole.Id);

                    break;

                case SystemMessage.CustomerExecutive:
                    await RemoveRoleFromUser(appUser, customerOwnerRole.Name);
                    await AddRoleToUser(appUser, customerAdminRole.Name);
                    await AddRoleToUser(appUser, customerAdminManagerRole.Name);
                    await RemoveRoleFromUser(appUser, customerManagerRole.Name);

                    RemoveRoleClaimFromClaim(employerIdUserClaim, customerOwnerRole.Id);
                    AddRoleClaimToClaim(employerIdUserClaim, customerAdminRole.Id);
                    AddRoleClaimToClaim(employerIdUserClaim, customerAdminManagerRole.Id);
                    RemoveRoleClaimFromClaim(employerIdUserClaim, customerManagerRole.Id);

                    break;

                case SystemMessage.CustomerExecutiveManager:
                    await RemoveRoleFromUser(appUser, customerOwnerRole.Name);
                    await RemoveRoleFromUser(appUser, customerAdminRole.Name);
                    await AddRoleToUser(appUser, customerAdminManagerRole.Name);
                    await RemoveRoleFromUser(appUser, customerManagerRole.Name);

                    RemoveRoleClaimFromClaim(employerIdUserClaim, customerOwnerRole.Id);
                    RemoveRoleClaimFromClaim(employerIdUserClaim, customerAdminRole.Id);
                    AddRoleClaimToClaim(employerIdUserClaim, customerAdminManagerRole.Id);
                    RemoveRoleClaimFromClaim(employerIdUserClaim, customerManagerRole.Id);

                    break;

                case SystemMessage.CustomerAssistant:
                    await RemoveRoleFromUser(appUser, customerOwnerRole.Name);
                    await RemoveRoleFromUser(appUser, customerAdminRole.Name);
                    await RemoveRoleFromUser(appUser, customerAdminManagerRole.Name);
                    await AddRoleToUser(appUser, customerManagerRole.Name);

                    RemoveRoleClaimFromClaim(employerIdUserClaim, customerOwnerRole.Id);
                    RemoveRoleClaimFromClaim(employerIdUserClaim, customerAdminRole.Id);
                    RemoveRoleClaimFromClaim(employerIdUserClaim, customerAdminManagerRole.Id);
                    AddRoleClaimToClaim(employerIdUserClaim, customerManagerRole.Id);

                    break;
            }
            await _userManager.UpdateAsync(appUser);
        }

        /// <summary>
        /// removed roles and claims
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="employerId"></param>
        /// <returns></returns>
        public async Task DeleteEmployerManagerRolesAndClaims(
            AppUser appUser,
            int employerId)
        {
            var rolesToRemove = new List<string> { Authorize.Roles.CustomerOwner, Authorize.Roles.CustomerAdmin, Authorize.Roles.CustomerAdminManager, Authorize.Roles.CustomerManager };

            var employerIdUserClaim = await GetEmployerIdClaim(
                appUser,
                employerId);

            var customerOwnerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CustomerOwner).First();
            var customerAdminRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CustomerAdmin).First();
            var customerAdminManagerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CustomerAdminManager).First();
            var customerManagerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CustomerManager).First();
            var employeeRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.Employee).First();

            var otherEmployerIdClaims = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimEmployerId && x.ClaimValue != employerId.ToString()).ToList();

            if (otherEmployerIdClaims.Count > 0)
            {
                foreach (var employerClaim in otherEmployerIdClaims)
                {
                    var userClaimRole = employerClaim.UserClaimRoles.Where(x => x.RoleId == customerOwnerRole.Id).FirstOrDefault();
                    if (rolesToRemove.Contains(userClaimRole.Role.Name))
                        rolesToRemove.Remove(userClaimRole.Role.Name);
                    var userClaimRole1 = employerClaim.UserClaimRoles.Where(x => x.RoleId == customerAdminRole.Id).FirstOrDefault();
                    if (rolesToRemove.Contains(userClaimRole1.Role.Name))
                        rolesToRemove.Remove(userClaimRole1.Role.Name);
                    var userClaimRole2 = employerClaim.UserClaimRoles.Where(x => x.RoleId == customerAdminManagerRole.Id).FirstOrDefault();
                    if (rolesToRemove.Contains(userClaimRole2.Role.Name))
                        rolesToRemove.Remove(userClaimRole2.Role.Name);
                    var userClaimRole3 = employerClaim.UserClaimRoles.Where(x => x.RoleId == customerManagerRole.Id).FirstOrDefault();
                    if (rolesToRemove.Contains(userClaimRole3.Role.Name))
                        rolesToRemove.Remove(userClaimRole3.Role.Name);
                }
            }

            if (rolesToRemove.Count > 0)
            {
                foreach (var roleToRemove in rolesToRemove)
                {
                    if (appUser.UserRoles.Where(x => x.Role.Name == roleToRemove).FirstOrDefault() != null)
                        await _userManager.RemoveFromRoleAsync(appUser, roleToRemove);
                }
            }

            appUser = await GetFullUserById(appUser.Id);

            var employerUserClaimCustomerOwnerRole = employerIdUserClaim.UserClaimRoles.Where(x => x.RoleId == customerOwnerRole.Id).FirstOrDefault();
            if (employerUserClaimCustomerOwnerRole != null)
                employerIdUserClaim.UserClaimRoles.Remove(employerUserClaimCustomerOwnerRole);
            var employerUserClaimCustomerAdminRole = employerIdUserClaim.UserClaimRoles.Where(x => x.RoleId == customerAdminRole.Id).FirstOrDefault();
            if (employerUserClaimCustomerAdminRole != null)
                employerIdUserClaim.UserClaimRoles.Remove(employerUserClaimCustomerAdminRole);
            var employerUserClaimCustomerAdminManagerRole = employerIdUserClaim.UserClaimRoles.Where(x => x.RoleId == customerAdminManagerRole.Id).FirstOrDefault();
            if (employerUserClaimCustomerAdminManagerRole != null)
                employerIdUserClaim.UserClaimRoles.Remove(employerUserClaimCustomerAdminManagerRole);
            var employerUserClaimCustomerManagerRole = employerIdUserClaim.UserClaimRoles.Where(x => x.RoleId == customerManagerRole.Id).FirstOrDefault();
            if (employerUserClaimCustomerManagerRole != null)
                employerIdUserClaim.UserClaimRoles.Remove(employerUserClaimCustomerManagerRole);

            var employerUserClaimEmployeeRole = employerIdUserClaim.UserClaimRoles.Where(x => x.RoleId == employeeRole.Id).FirstOrDefault();
            if (employerUserClaimEmployeeRole == null)
                appUser.Claims.Remove(employerIdUserClaim);

            await _userManager.UpdateAsync(appUser);
            appUser = await GetFullUserById(appUser.Id);

            if (otherEmployerIdClaims.Count < 1)
            {
                var accountIdSchemaClaim = await GetAccountSchema(appUser);
                appUser.Claims.Remove(accountIdSchemaClaim);
                var accountSchema = (Authorize.AccountLevel)Convert.ToInt32(accountIdSchemaClaim.ClaimValue);
                accountSchema ^= Authorize.AccountLevel.customer;
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimAccountSchema,
                    ClaimValue = ((int)accountSchema).ToString()
                });
            }

            var defaultSchemaClaim = await GetDefaultAccountSchema(appUser);
            var defaultSchema = (Authorize.AccountLevel)Convert.ToInt32(defaultSchemaClaim.ClaimValue);
            if ((defaultSchema & Authorize.AccountLevel.customer) == Authorize.AccountLevel.customer)
            {
                appUser.Claims.Remove(defaultSchemaClaim);
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimDefaultSchema,
                    ClaimValue = ((int)Authorize.AccountLevel.jobseeker).ToString()
                });
            }

            var defaultEmployerIdClaim = await GetDefaultEmployerIdClaim(appUser);
            if (defaultEmployerIdClaim != null)
            {
                if (defaultEmployerIdClaim.ClaimValue == employerId.ToString())
                {
                    if (employerUserClaimEmployeeRole == null)
                        appUser.Claims.Remove(defaultEmployerIdClaim);

                    if (otherEmployerIdClaims.Count > 0)
                    {
                        appUser.Claims.Add(new AppUserClaim
                        {
                            UserId = appUser.Id,
                            ClaimType = Authorize.Claims.ClaimDefaultEmployerId,
                            ClaimValue = otherEmployerIdClaims.First().ClaimValue
                        });
                    }
                }
            }
            await _userManager.UpdateAsync(appUser);
        }

        /// <summary>
        /// activated customer claims
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="employerName"></param>
        /// <param name="employerId"></param>
        /// <returns></returns>
        public async Task ActivateClaimsEmployerManager(
            AppUser appUser,
            string employerName,
            int employerId)
        {
            var employerIdClaim = await GetEmployerIdClaim(
                appUser,
                employerId);
            if (employerIdClaim == null)
            {
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimEmployerId,
                    ClaimValue = employerId.ToString(),
                    ClaimProperty = employerName
                });
            }

            var defaultEmployerIdClaim = await GetDefaultEmployerIdClaim(appUser);
            if (defaultEmployerIdClaim == null)
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimDefaultEmployerId,
                    ClaimValue = employerId.ToString(),
                    ClaimProperty = employerName
                });

            var accountSchemaClaim = await GetAccountSchema(appUser);
            var accountSchema = (Authorize.AccountLevel)Convert.ToInt32(accountSchemaClaim.ClaimValue);

            if (!((accountSchema & Authorize.AccountLevel.customer) == Authorize.AccountLevel.customer))
            {
                appUser.Claims.Remove(accountSchemaClaim);
                accountSchema |= Authorize.AccountLevel.customer;
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimAccountSchema,
                    ClaimValue = ((int)accountSchema).ToString()
                });
            }

            await _userManager.UpdateAsync(appUser);
        }
        #endregion

        #region Global account schema 
        /// <summary>
        /// returns current account schema for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<PersonAccountLevel>> GetAccountSchemaSettings(
            string userId)
        {
            var appUser = await GetFullUserById(userId);
            var accountSchema = (Authorize.AccountLevel)Convert.ToInt32(appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimAccountSchema).First().ClaimValue);
            var defaultAccountSchema = (Authorize.AccountLevel)Convert.ToInt32(appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultSchema).First().ClaimValue);
            var result = new List<PersonAccountLevel>();

            foreach (var level in Enum.GetValues(typeof(Authorize.AccountLevel)))
            {
                if ((Authorize.AccountLevel.none & (Authorize.AccountLevel)level) == (Authorize.AccountLevel)level)
                    continue;

                if (((accountSchema & (Authorize.AccountLevel)level) == (Authorize.AccountLevel)level) &&
                    (((Authorize.AccountLevel)level & Authorize.AccountLevel.admin) == Authorize.AccountLevel.admin))
                {
                    var accountLevel = new PersonAccountLevel
                    {
                        LevelName = ((Authorize.AccountLevel)level).ToString(),
                        LevelCode = (int)(Authorize.AccountLevel)level
                    };

                    if ((defaultAccountSchema & (Authorize.AccountLevel)level) == (Authorize.AccountLevel)level)
                        accountLevel.IsSet = true;

                    accountLevel.CompanyEmployers = null;
                    result.Add(accountLevel);
                    continue;
                }

                if (((accountSchema & (Authorize.AccountLevel)level) == (Authorize.AccountLevel)level) &&
                    (((Authorize.AccountLevel)level & Authorize.AccountLevel.company) == Authorize.AccountLevel.company))
                {
                    var accountLevel = new PersonAccountLevel
                    {
                        LevelName = ((Authorize.AccountLevel)level).ToString(),
                        LevelCode = (int)(Authorize.AccountLevel)level
                    };

                    if ((defaultAccountSchema & (Authorize.AccountLevel)level) == (Authorize.AccountLevel)level)
                        accountLevel.IsSet = true;

                    var defaultAccountId = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultAccountId).First().ClaimValue;

                    accountLevel.CompanyEmployers = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimAccountId)
                        .Where(k => k.UserClaimRoles.Count > 0).Select(y => new CompanyEmployerProperty
                        {
                            AccountId = y.ClaimValue,
                            CompanyName = y.ClaimProperty,
                            EmployerId = null,
                            EmployerName = null,
                            IsSet = y.ClaimValue == defaultAccountId
                        }).ToList();

                    result.Add(accountLevel);
                    continue;
                }

                if (((accountSchema & (Authorize.AccountLevel)level) == (Authorize.AccountLevel)level) &&
                    (((Authorize.AccountLevel)level & Authorize.AccountLevel.customer) == Authorize.AccountLevel.customer))
                {
                    var accountLevel = new PersonAccountLevel
                    {
                        LevelName = ((Authorize.AccountLevel)level).ToString(),
                        LevelCode = (int)(Authorize.AccountLevel)level
                    };

                    if ((defaultAccountSchema & (Authorize.AccountLevel)level) == (Authorize.AccountLevel)level)
                        accountLevel.IsSet = true;

                    var employerIdClaims = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimEmployerId)
                        .Where(k => k.UserClaimRoles.Count > 0).ToList();

                    var customerOnly = employerIdClaims.Where(x => x.UserClaimRoles.Where(
                        y => y.Role.Name == Authorize.Roles.CustomerOwner
                        || y.Role.Name == Authorize.Roles.CustomerAdmin
                        || y.Role.Name == Authorize.Roles.CustomerAdminManager
                        || y.Role.Name == Authorize.Roles.CustomerManager).Count() > 0).ToList();

                    var defaultEmployerId = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultEmployerId).First().ClaimValue;

                    accountLevel.CompanyEmployers = customerOnly.Select(y => new CompanyEmployerProperty
                    {
                        AccountId = null,
                        CompanyName = null,
                        EmployerId = y.ClaimValue,
                        EmployerName = y.ClaimProperty,
                        IsSet = y.ClaimValue == defaultEmployerId
                    }).ToList();

                    //foreach (var companyEmployer in accountLevel.CompanyEmployers)
                    //    companyEmployer.AccountId = (await _employerCompany.GetBySpecAsync(
                    //        new EmployerCompanySpec(Convert.ToInt32(companyEmployer.EmployerId)))).CompanyId.ToString();

                    result.Add(accountLevel);
                    continue;
                }

                if (((accountSchema & (Authorize.AccountLevel)level) == (Authorize.AccountLevel)level) &&
                    (((Authorize.AccountLevel)level & Authorize.AccountLevel.employee) == Authorize.AccountLevel.employee))
                {
                    var accountLevel = new PersonAccountLevel
                    {
                        LevelName = ((Authorize.AccountLevel)level).ToString(),
                        LevelCode = (int)(Authorize.AccountLevel)level
                    };

                    if ((defaultAccountSchema & (Authorize.AccountLevel)level) == (Authorize.AccountLevel)level)
                        accountLevel.IsSet = true;

                    var employerIdClaims = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimEmployerId)
                        .Where(k => k.UserClaimRoles.Count > 0).ToList();

                    var employeeOnly = employerIdClaims.Where(x => x.UserClaimRoles.Where(
                        y => y.Role.Name == Authorize.Roles.Employee).Count() > 0).ToList();

                    var defaultEmployerId = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultEmployerId).First().ClaimValue;

                    accountLevel.CompanyEmployers = employeeOnly.Select(y => new CompanyEmployerProperty
                    {
                        AccountId = null,
                        CompanyName = null,
                        EmployerId = y.ClaimValue,
                        EmployerName = y.ClaimProperty,
                        IsSet = y.ClaimValue == defaultEmployerId
                    }).ToList();

                    //foreach (var companyEmployer in accountLevel.CompanyEmployers)
                    //    companyEmployer.AccountId = (await _employerCompany.GetBySpecAsync(
                    //        new EmployerCompanySpec(Convert.ToInt32(companyEmployer.EmployerId)))).CompanyId.ToString();

                    result.Add(accountLevel);
                    continue;
                }

                if (((accountSchema & (Authorize.AccountLevel)level) == (Authorize.AccountLevel)level) &&
                    (((Authorize.AccountLevel)level & Authorize.AccountLevel.jobseeker) == Authorize.AccountLevel.jobseeker))
                {
                    var accountLevel = new PersonAccountLevel
                    {
                        LevelName = ((Authorize.AccountLevel)level).ToString(),
                        LevelCode = (int)(Authorize.AccountLevel)level
                    };

                    if ((defaultAccountSchema & (Authorize.AccountLevel)level) == (Authorize.AccountLevel)level)
                        accountLevel.IsSet = true;

                    accountLevel.CompanyEmployers = null;
                    result.Add(accountLevel);
                    continue;
                }
            }

            return result;
        }

        /// <summary>
        /// updates account schema for user
        /// </summary>
        /// <param name="personAccountLevel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> PutAccountSchemaSettings(
            PersonAccountLevel personAccountLevel,
            string userId)
        {
            var appUser = await GetFullUserById(userId);
            var defaultAccountSchema = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultSchema).First();
            var newDefaultAccountSchema = personAccountLevel.LevelCode;
            appUser.Claims.Remove(defaultAccountSchema);
            appUser.Claims.Add(new AppUserClaim
            {
                UserId = appUser.Id,
                ClaimType = Authorize.Claims.ClaimDefaultSchema,
                ClaimValue = newDefaultAccountSchema.ToString()
            });

            await _userManager.UpdateAsync(appUser);

            if (personAccountLevel.CompanyEmployers == null)
                return "Default account level is changed";
               
            var newDefaultProp = personAccountLevel.CompanyEmployers.Where(x => x.IsSet).FirstOrDefault();
            if (newDefaultProp == null)
                return "Something went wrong";
                
            if (personAccountLevel.LevelCode == (int)Authorize.AccountLevel.customer || personAccountLevel.LevelCode == (int)Authorize.AccountLevel.employee)
            {
                var oldDefaultEmployerIdClaim = await GetDefaultEmployerIdClaim(appUser);
                if (oldDefaultEmployerIdClaim != null)
                {
                    appUser.Claims.Remove(oldDefaultEmployerIdClaim);
                    appUser.Claims.Add(new AppUserClaim
                    {
                        UserId = appUser.Id,
                        ClaimType = Authorize.Claims.ClaimDefaultEmployerId,
                        ClaimValue = newDefaultProp.EmployerId,
                        ClaimProperty = newDefaultProp.EmployerName
                    });
                }
            }
            else if (personAccountLevel.LevelCode == (int)Authorize.AccountLevel.company)
            {
                var oldDefaultCompanyIdClaim = await GetDefaultAccountIdClaim(appUser);
                if (oldDefaultCompanyIdClaim != null)
                {
                    appUser.Claims.Remove(oldDefaultCompanyIdClaim);
                    appUser.Claims.Add(new AppUserClaim
                    {
                        UserId = appUser.Id,
                        ClaimType = Authorize.Claims.ClaimDefaultAccountId,
                        ClaimValue = newDefaultProp.AccountId,
                        ClaimProperty = newDefaultProp.CompanyName
                    });
                }
            }

            await _userManager.UpdateAsync(appUser);

            return  "Default account level is changed";
           
        }
        #endregion

        public async Task<List<ApplyManager>> GetUsersForClaimAsync(
            int accountId)
        {
            var users = await _userManager.GetUsersForClaimAsync(
                new Claim(Authorize.Claims.ClaimAccountId, accountId.ToString()));

            var result = users.Select(x => new ApplyManager
            {
                Name = x.Person.FirstName + " " + x.Person.LastName,
                Email = x.Person.EmailAddress
            }).ToList();

            return result;
        }

        public async Task<AppUser> ChangeUserEmail(
            AppUser appUser,
            string email)
        {
            var token = await _userManager.GenerateChangeEmailTokenAsync(appUser, email);
            await _userManager.ChangeEmailAsync(appUser, email, token);
            await _userManager.SetUserNameAsync(appUser, email);
            await _userManager.UpdateNormalizedEmailAsync(appUser);
            await _userManager.UpdateNormalizedUserNameAsync(appUser);

            return appUser;
        }

        public async Task<IdentityResult> ResetUserEmail(
            AppUser appUser,
            string email,
            string resetEmailToken)
        {
            await _userManager.ChangeEmailAsync(appUser, email, resetEmailToken);
            await _userManager.UpdateNormalizedEmailAsync(appUser);
            await _userManager.UpdateNormalizedUserNameAsync(appUser);

            return await _userManager.SetUserNameAsync(appUser, email);
        }

        public async Task<AppUser> AddCustomerOwnerUserInRegister(
            string email,
            string password,
            string employerName,
            int employerId,
            AppPerson person)
        {
            var appUser = new AppUser { Email = email, UserName = email, Person = person };
            var result = await _userManager.CreateAsync(appUser, password);
            if (!result.Succeeded)
                return null;

            await _userManager.AddToRolesAsync(appUser, new[] { Authorize.Roles.CustomerOwner, Authorize.Roles.CustomerAdmin, Authorize.Roles.CustomerManager, Authorize.Roles.JobSeeker });
            appUser = await GetFullUserById(appUser.Id);
            var accountLevel = Authorize.AccountLevel.customer | Authorize.AccountLevel.jobseeker;

            appUser.Claims.Add(new AppUserClaim
            {
                UserId = appUser.Id,
                ClaimType = Authorize.Claims.ClaimAccountSchema,
                ClaimValue = ((int)accountLevel).ToString()
            });

            appUser.Claims.Add(new AppUserClaim
            {
                UserId = appUser.Id,
                ClaimType = Authorize.Claims.ClaimDefaultSchema,
                ClaimValue = ((int)Authorize.AccountLevel.customer).ToString()
            });

            appUser.Claims.Add(new AppUserClaim
            {
                UserId = appUser.Id,
                ClaimType = Authorize.Claims.ClaimEmployerId,
                ClaimValue = employerId.ToString(),
                ClaimProperty = employerName
            });

            appUser.Claims.Add(new AppUserClaim
            {
                UserId = appUser.Id,
                ClaimType = Authorize.Claims.ClaimDefaultEmployerId,
                ClaimValue = employerId.ToString(),
                ClaimProperty = employerName
            });

            await _userManager.UpdateAsync(appUser);
            appUser = await GetFullUserById(appUser.Id);

            var employerIdUserClaim = await GetEmployerIdClaim(
                appUser,
                employerId);

            var customerOwnerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CustomerOwner).First();
            var customerAdminRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CustomerAdmin).First();
            var customerAdminManagerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CustomerAdminManager).First();
            var customerManagerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CustomerManager).First();

            await AddRoleToUser(appUser, customerOwnerRole.Name);
            await AddRoleToUser(appUser, customerAdminRole.Name);
            await AddRoleToUser(appUser, customerAdminManagerRole.Name);
            await AddRoleToUser(appUser, customerManagerRole.Name);

            AddRoleClaimToClaim(employerIdUserClaim, customerOwnerRole.Id);
            AddRoleClaimToClaim(employerIdUserClaim, customerAdminRole.Id);
            AddRoleClaimToClaim(employerIdUserClaim, customerAdminManagerRole.Id);
            AddRoleClaimToClaim(employerIdUserClaim, customerManagerRole.Id);

            await _userManager.UpdateAsync(appUser);
            appUser = await GetFullUserById(appUser.Id);
            return appUser;
        }

        public async Task<AppUser> AddCompanyOwnerUserInRegister(
            string email,
            string password,
            int companyId,
            string companyName,
            AppPerson person)
        {
            var appUser = new AppUser { Email = email, UserName = email, Person = person };
            var result = await _userManager.CreateAsync(appUser, password);
            if (!result.Succeeded)
                return null;

            await _userManager.AddToRolesAsync(appUser, new[] { Authorize.Roles.CompanyOwner, Authorize.Roles.CompanyAdmin, Authorize.Roles.CompanyManager, Authorize.Roles.JobSeeker });
            appUser = await GetFullUserById(appUser.Id);
            var accountLevel = Authorize.AccountLevel.company | Authorize.AccountLevel.jobseeker;

            appUser.Claims.Add(new AppUserClaim
            {
                UserId = appUser.Id,
                ClaimType = Authorize.Claims.ClaimAccountSchema,
                ClaimValue = ((int)accountLevel).ToString()
            });

            appUser.Claims.Add(new AppUserClaim
            {
                UserId = appUser.Id,
                ClaimType = Authorize.Claims.ClaimDefaultSchema,
                ClaimValue = ((int)Authorize.AccountLevel.company).ToString()
            });

            appUser.Claims.Add(new AppUserClaim
            {
                UserId = appUser.Id,
                ClaimType = Authorize.Claims.ClaimAccountId,
                ClaimValue = companyId.ToString(),
                ClaimProperty = companyName
            });

            appUser.Claims.Add(new AppUserClaim
            {
                UserId = appUser.Id,
                ClaimType = Authorize.Claims.ClaimDefaultAccountId,
                ClaimValue = companyId.ToString(),
                ClaimProperty = companyName
            });

            await _userManager.UpdateAsync(appUser);
            appUser = await GetFullUserById(appUser.Id);
            var accountIdUserClaim = await GetAccountIdClaim(
                appUser,
                companyId);
            var companyOwnerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CompanyOwner).First();
            var companyAdminRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CompanyAdmin).First();
            var companyManagerRole = _roleManager.Roles.Where(x => x.Name == Authorize.Roles.CompanyManager).First();
            AddRoleClaimToClaim(accountIdUserClaim, companyOwnerRole.Id);
            AddRoleClaimToClaim(accountIdUserClaim, companyAdminRole.Id);
            AddRoleClaimToClaim(accountIdUserClaim, companyManagerRole.Id);

            await _userManager.UpdateAsync(appUser);
            appUser = await GetFullUserById(appUser.Id);

            var personNew=  await _person.AddAsync(new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                DisplayName = person.DisplayName,
                Dob = person.Dob,
                GenderSysCodeUniqueId = person.GenderSysCodeUniqueId,
                EmailAddress = person.EmailAddress
            });
            appUser.PersonId = personNew.PersonId;

            return appUser;
        }

        public async Task<AppUser> AddJobSeekerUserInRegister(
            string email,
            string password,
            AppPerson person)
        {
            var appUser = new AppUser { Email = email, UserName = email, Person = person };
            var result = await _userManager.CreateAsync(appUser, password);
            if (!result.Succeeded)
                return null;

            await _userManager.AddToRolesAsync(appUser, new[] { Authorize.Roles.JobSeeker });
            appUser = await GetFullUserById(appUser.Id);
            var accountLevel = Authorize.AccountLevel.jobseeker;

            appUser.Claims.Add(new AppUserClaim
            {
                UserId = appUser.Id,
                ClaimType = Authorize.Claims.ClaimAccountSchema,
                ClaimValue = ((int)accountLevel).ToString()
            });

            appUser.Claims.Add(new AppUserClaim
            {
                UserId = appUser.Id,
                ClaimType = Authorize.Claims.ClaimDefaultSchema,
                ClaimValue = ((int)accountLevel).ToString()
            });

            await _userManager.UpdateAsync(appUser);
            appUser = await GetFullUserById(appUser.Id);
            return appUser;
        }

        public async Task<IList<UserLoginInfo>> GetUserLogins(
            AppUser appUser)
        {
            return await _userManager.GetLoginsAsync(appUser);
        }

        public async Task<IdentityResult> RemoveUserLoginInfo(
            AppUser appUser,
            string providerName,
            string providerKey)
        {
            return await _userManager.RemoveLoginAsync(
                appUser,
                providerName,
                providerKey);
        }

        public async Task<IdentityResult> ConfirmUserEmail(
            AppUser appUser)
        {
            appUser.EmailConfirmed = true;
            return await _userManager.UpdateAsync(appUser);
        }

        #region Service
        /// <summary>
        /// service methods
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        private async Task<AppUserClaim> GetAccountSchema(
            AppUser appUser)
        {
            var accountSchemaClaim = new AppUserClaim();
            var accountSchemaClaims = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimAccountSchema).ToList();
            if (accountSchemaClaims.Count > 1)
            {
                accountSchemaClaim = accountSchemaClaims.Last();
                accountSchemaClaims = accountSchemaClaims.SkipLast(1).ToList();
                foreach (var claim in accountSchemaClaims)
                    appUser.Claims.Remove(claim);
            }
            else if (accountSchemaClaims.Count == 1)
                accountSchemaClaim = accountSchemaClaims.First();
            else
            {
                if (appUser.UserRoles.Where(x => x.Role.Name == Authorize.Roles.JobSeeker).FirstOrDefault() == null)
                    await _userManager.AddToRoleAsync(appUser, Authorize.Roles.JobSeeker);
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimAccountSchema,
                    ClaimValue = ((int)Authorize.AccountLevel.jobseeker).ToString()
                });

                await _userManager.UpdateAsync(appUser);

                appUser = await _userManager.Users
                    .Where(x => x.Id == appUser.Id)
                    .Include(e => e.Claims)
                    .FirstOrDefaultAsync();

                accountSchemaClaim = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimAccountSchema).First();
            }
            return accountSchemaClaim;
        }

        private async Task<AppUserClaim> GetDefaultAccountSchema(
            AppUser appUser)
        {
            var defaultAccountSchemaClaim = new AppUserClaim();
            var defaultAccountSchemaClaims = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultSchema).ToList();
            if (defaultAccountSchemaClaims.Count > 1)
            {
                defaultAccountSchemaClaim = defaultAccountSchemaClaims.Last();
                defaultAccountSchemaClaims = defaultAccountSchemaClaims.SkipLast(1).ToList();
                foreach (var claim in defaultAccountSchemaClaims)
                    appUser.Claims.Remove(claim);
            }
            else if (defaultAccountSchemaClaims.Count == 1)
                defaultAccountSchemaClaim = defaultAccountSchemaClaims.First();
            else
            {
                if (appUser.UserRoles.Where(x => x.Role.Name == Authorize.Roles.JobSeeker).FirstOrDefault() == null)
                    await _userManager.AddToRoleAsync(appUser, Authorize.Roles.JobSeeker);
                appUser.Claims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = Authorize.Claims.ClaimDefaultSchema,
                    ClaimValue = ((int)Authorize.AccountLevel.jobseeker).ToString()
                });

                await _userManager.UpdateAsync(appUser);

                appUser = await _userManager.Users
                        .Where(x => x.Id == appUser.Id)
                        .Include(e => e.Claims)
                        .FirstOrDefaultAsync();

                defaultAccountSchemaClaim = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultSchema).First();
            }
            return defaultAccountSchemaClaim;
        }

        private async Task<AppUserClaim> GetAccountIdClaim(
            AppUser appUser,
            int accountId)
        {
            var allaccountIdClaims = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimAccountId).ToList();
            if (allaccountIdClaims.Count < 1)
                return null;
            var allaccountIds = allaccountIdClaims.Select(x => x.ClaimValue).Distinct().ToList();
            foreach (var id in allaccountIds)
            {
                var idClaims = allaccountIdClaims.Where(x => x.ClaimValue == id).ToList();
                if (idClaims.Count > 1)
                {
                    idClaims = idClaims.SkipLast(1).ToList();
                    foreach (var i in idClaims)
                        appUser.Claims.Remove(i);
                }
            }
            await _userManager.UpdateAsync(appUser);
            appUser = await GetFullUserById(appUser.Id);
            return appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimAccountId && x.ClaimValue == accountId.ToString()).FirstOrDefault();
        }

        private async Task<AppUserClaim> GetDefaultAccountIdClaim(
            AppUser appUser)
        {
            var defaultAccountIdClaims = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultAccountId).ToList();
            if (defaultAccountIdClaims.Count < 1)
                return null;
            if (defaultAccountIdClaims.Count > 1)
            {
                defaultAccountIdClaims = defaultAccountIdClaims.SkipLast(1).ToList();
                foreach (var claim in defaultAccountIdClaims)
                    appUser.Claims.Remove(claim);
            }
            await _userManager.UpdateAsync(appUser);
            appUser = await GetFullUserById(appUser.Id);
            return appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultAccountId).FirstOrDefault();
        }

        private async Task<AppUserClaim> GetEmployerIdClaim(
            AppUser appUser,
            int employerId)
        {
            var allemployerIdClaims = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimEmployerId).ToList();
            if (allemployerIdClaims.Count < 1)
                return null;
            var allemployerIds = allemployerIdClaims.Select(x => x.ClaimValue).Distinct().ToList();
            foreach (var id in allemployerIds)
            {
                var idClaims = allemployerIdClaims.Where(x => x.ClaimValue == id).ToList();
                if (idClaims.Count > 1)
                {
                    idClaims = idClaims.SkipLast(1).ToList();
                    foreach (var i in idClaims)
                        appUser.Claims.Remove(i);
                }
            }
            await _userManager.UpdateAsync(appUser);
            appUser = await GetFullUserById(appUser.Id);
            return appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimEmployerId && x.ClaimValue == employerId.ToString()).FirstOrDefault();
        }

        private async Task<AppUserClaim> GetDefaultEmployerIdClaim(
            AppUser appUser)
        {
            var defaultEmployerIdClaims = appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultEmployerId).ToList();
            if (defaultEmployerIdClaims.Count < 1)
                return null;
            if (defaultEmployerIdClaims.Count > 1)
            {
                defaultEmployerIdClaims = defaultEmployerIdClaims.SkipLast(1).ToList();
                foreach (var claim in defaultEmployerIdClaims)
                    appUser.Claims.Remove(claim);
            }
            await _userManager.UpdateAsync(appUser);
            appUser = await GetFullUserById(appUser.Id);
            return appUser.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultEmployerId).FirstOrDefault();
        }

        private async Task AddRoleToUser(
            AppUser appUser,
            string role)
        {
            if (appUser.UserRoles.Where(x => x.Role.Name == role).FirstOrDefault() == null)
                await _userManager.AddToRoleAsync(appUser, role);
        }

        private void AddRoleClaimToClaim(
            AppUserClaim appClaim,
            string roleId)
        {
            if (appClaim.UserClaimRoles.Where(x => x.RoleId == roleId).FirstOrDefault() == null)
                appClaim.UserClaimRoles.Add(new AspNetUserClaimRole
                {
                    RoleId = roleId,
                    ClaimId = appClaim.Id
                });
        }

        private async Task RemoveRoleFromUser(
            AppUser appUser,
            string role)
        {
            if (appUser.UserRoles.Where(x => x.Role.Name == role).FirstOrDefault() == null)
                await _userManager.RemoveFromRoleAsync(appUser, role);
        }

        private void RemoveRoleClaimFromClaim(
            AppUserClaim appClaim,
            string roleId)
        {
            var claimRole = appClaim.UserClaimRoles.Where(x => x.RoleId == roleId).FirstOrDefault();
            if (claimRole != null)
                appClaim.UserClaimRoles.Remove(claimRole);
        }
        #endregion
    }
}
