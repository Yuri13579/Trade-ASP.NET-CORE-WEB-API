using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using _1U_ASP.Security.Model;
using Microsoft.AspNetCore.Identity;

namespace _1U_ASP.Security.Service
{
    public interface IUserAccountProcessing
    {
        Task<AppUser> AddCompanyOwnerUserInRegister(
            string email,
            string password,
            int companyId,
            string companyName,
            AppPerson person);
        
        Task<AppUser> GetFullUserByEmail(
            string email);

        //Task<string> GetRoleIdByName(
        //    string name);

        //Task<AppUser> GetFullUserById(
        //    string id);

        //Task<AppUser> GetFullUserByPersonId(
        //    int id);

        //Task<AppUser> GetFullUserByJwtSecurityToken(
        //    JwtSecurityToken tokenJwt);

        //Task<AppUser> GetFullUserByLoginProvider(
        //    string loginProvider,
        //    string providerKey);

        //Task<IdentityResult> AddLoginProvider(
        //    AppUser appUser,
        //    UserLoginInfo userLoginInfo);

        //Task<IdentityResult> AddToRole(
        //    AppUser appUser,
        //    string roleName);

        //Task<IdentityResult> RemoveFromRole(
        //    AppUser appUser,
        //    string roleName);

        //Task<bool> IsInRole(
        //    AppUser appUser,
        //    string roleName);

        //Task<AppUser> AddUser(
        //    string email,
        //    int personId);

        //Task AddJobSeekerAccount(
        //    AppUser appUserr);

        //Task AddEmployeeClaims(
        //    AppUser appUser,
        //    string employerName,
        //    int employerId);

        //Task AddCompanyManagerClaims(
        //    AppUser appUser,
        //    int accountId,
        //    string companyName);

        //Task RoleUpdateCompanyManager(
        //    string profilePosition,
        //    int accountId,
        //    AppUser appUser);

        //Task DeleteCompanyManagerRolesAndClaims(
        //    AppUser appUser,
        //    int accountId);

        //Task ActivateClaimsCompanyManager(
        //    AppUser appUser,
        //    int accountId,
        //    string companyName);

        //Task AddEmployerManagerClaims(
        //    AppUser appUser,
        //    string employerName,
        //    int employerId);

        //Task RoleUpdateEmployerManager(
        //    string adminLevel,
        //    int employerId,
        //    AppUser appUser);

        //Task DeleteEmployerManagerRolesAndClaims(
        //    AppUser appUser,
        //    int employerId);

        //Task ActivateClaimsEmployerManager(
        //    AppUser appUser,
        //    string employerName,
        //    int employerId);

        //Task<AppUser> ChangeUserEmail(
        //    AppUser appUser,
        //    string email);

        //Task<IdentityResult> ConfirmUserEmail(
        //    AppUser appUser);

        //Task<IdentityResult> ResetUserEmail(
        //    AppUser appUser,
        //    string email,
        //    string resetEmailToken);

        //Task<List<PersonAccountLevel>> GetAccountSchemaSettings(
        //    string userId);

        //Task<string> PutAccountSchemaSettings(
        //    PersonAccountLevel personAccountLevel,
        //    string userId);
        
        //Task<AppUser> AddJobSeekerUserInRegister(
        //    string email,
        //    string password,
        //    AppPerson person);

        //Task<IList<UserLoginInfo>> GetUserLogins(
        //    AppUser appUser);

        //Task<IdentityResult> RemoveUserLoginInfo(
        //    AppUser appUser,
        //    string providerName,
        //    string providerKey);

        //Task<List<ApplyManager>> GetUsersForClaimAsync(
        //    int accountId);

        //Task<AppUser> AddCustomerOwnerUserInRegister(
        //    string email,
        //    string password,
        //    string employerName,
        //    int employerId,
        //    AppPerson person);
    }
}
