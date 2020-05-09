using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using _1U_ASP.Const;
using _1U_ASP.Context;
using _1U_ASP.Security.Model;
using Microsoft.IdentityModel.Tokens;

namespace _1U_ASP.Security.Service
{
    public static class TokenProcessing
    {
        private static SigningCredentials SigningCredentials { get; } = GlobalVariables.SigningCredentials;
        private static string ClientHost { get; } = GlobalVariables.ClientHost;

        public static JwtSecurityToken GetJwtSecurityToken(
            AppUser user,
            string remoteIpAddress)
        {
            var defaultSchema = (Authorize.AccountLevel)Convert.ToInt32(user.Claims
                .Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultSchema).First().ClaimValue);

            return new JwtSecurityToken(
                GlobalVariables.Issuer,
                GlobalVariables.Audience,
                claims: GetTokenClaims(
                    user,
                    defaultSchema,
                    null,
                    null,
                    remoteIpAddress),
                expires: DateTime.UtcNow.Add(new TimeSpan(0, 12, 0, 0)),
                notBefore: DateTime.UtcNow,
                signingCredentials: SigningCredentials);
        }

        public static JwtSecurityToken GetJwtSecurityTokenAsCustomerOwner(
            AppUser appUser,
            int personId,
            int employerId,
            string remoteIpAddress)
        {
            var userClaims = appUser.Claims;

            return new JwtSecurityToken(
                GlobalVariables.Issuer,
                GlobalVariables.Audience,
                claims: GetAsCustomerTokenClaims(
                    appUser.Id,
                    appUser.Email,
                    employerId,
                    userClaims.ToList(),
                    personId,
                    remoteIpAddress),
                expires: DateTime.UtcNow.Add(new TimeSpan(0, 12, 0, 0)),
                notBefore: DateTime.UtcNow,
                signingCredentials: SigningCredentials);
        }

        public static JwtSecurityToken GetRefreshJwtSecurityToken(
            Authorize.AccountLevel requestedLevel,
            AppUser user,
            string accountId,
            string employerId,
            string remoteIpAddress)
        {
            return new JwtSecurityToken(
                GlobalVariables.Issuer,
                GlobalVariables.Audience,
                claims: GetTokenClaims(
                    user,
                    requestedLevel,
                    accountId,
                    employerId,
                    remoteIpAddress),
                expires: DateTime.UtcNow.Add(new TimeSpan(0, 12, 0, 0)),
                notBefore: DateTime.UtcNow,
                signingCredentials: SigningCredentials);
        }

        public static JwtSecurityToken GetProlongJwtSecurityToken(
            JwtSecurityToken jwtSecurityToken)
        {
            return new JwtSecurityToken(
                jwtSecurityToken.Issuer,
                GlobalVariables.Audience,
                claims: jwtSecurityToken.Claims,
                expires: DateTime.UtcNow.Add(new TimeSpan(0, 12, 0, 0)),
                notBefore: DateTime.UtcNow,
                signingCredentials: SigningCredentials);
        }

        public static JwtSecurityToken GetJwtConfirmEmailToken(
            AppUser user,
            string remoteIpAddress,
            string returnUrl)
        {
            return new JwtSecurityToken(
                GlobalVariables.Issuer,
                GlobalVariables.Audience,
                claims: GetTokenClaimsConfirmEmail(remoteIpAddress, user, returnUrl),
                expires: DateTime.UtcNow.Add(new TimeSpan(1, 0, 0, 0)),
                notBefore: DateTime.UtcNow,
                signingCredentials: SigningCredentials);
        }

        public static JwtSecurityToken GetJwtResetPasswordToken(
            AppUser user,
            string remoteIpAddress,
            string resetPassToken)
        {
            return new JwtSecurityToken(
                GlobalVariables.Issuer,
                GlobalVariables.Audience,
                claims: GetTokenClaimsResetPassword(remoteIpAddress, user, resetPassToken),
                expires: DateTime.UtcNow.Add(new TimeSpan(0, 12, 0, 0)),
                notBefore: DateTime.UtcNow,
                signingCredentials: SigningCredentials);
        }

        public static JwtSecurityToken GetJwtResetEmailToken(
            AppUser user,
            string remoteIpAddress,
            string newEmail,
            string resetEmailToken)
        {
            return new JwtSecurityToken(
                GlobalVariables.Issuer,
                GlobalVariables.Audience,
                claims: GetTokenClaimsResetEmail(remoteIpAddress, user, newEmail, resetEmailToken),
                expires: DateTime.UtcNow.Add(new TimeSpan(0, 12, 0, 0)),
                notBefore: DateTime.UtcNow,
                signingCredentials: SigningCredentials);
        }

        public static JwtSecurityToken GetJwtSecurityNoteHubToken(
            string eventName)
        {
            return new JwtSecurityToken(
                GlobalVariables.Issuer,
                GlobalVariables.Audience,
                claims: GetTokenNoteHubClaims(eventName),
                expires: DateTime.UtcNow.Add(new TimeSpan(0, 12, 0, 0)),
                notBefore: DateTime.UtcNow,
                signingCredentials: SigningCredentials);
        }

        private static IEnumerable<Claim> GetTokenClaims(
            AppUser user,
            Authorize.AccountLevel requestedLevel,
            string accountId,
            string employerId,
            string remoteIpAddress)
        {
            var tokenClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(Authorize.Claims.ClaimIp, remoteIpAddress),
                new Claim(Authorize.Claims.ClaimPersonId, user.PersonId.ToString()),
                new Claim(Authorize.Claims.ClaimCurrentSchema, ((int)requestedLevel).ToString()),
                new Claim(Authorize.Claims.ClaimAccountSchema, user.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimAccountSchema).First().ClaimValue)
            };

            var roleClaims = new List<Claim>();

            if (string.IsNullOrEmpty(accountId) && string.IsNullOrEmpty(employerId))
                roleClaims = GetRoleClaims(
                    user,
                    requestedLevel);
            else roleClaims = GetRoleClaimsWithCompanyOrEmployer(
                user,
                accountId,
                employerId,
                requestedLevel);

            if (roleClaims.Count > 0)
                tokenClaims.AddRange(roleClaims);

            return tokenClaims;
        }

        private static IEnumerable<Claim> GetAsCustomerTokenClaims(
            string userId,
            string userEmail,
            int employerId,
            IList<AppUserClaim> userClaims,
            int personId,
            string remoteIpAddress)
        {
            var accountSchema = userClaims.Where(x => x.ClaimType == Authorize.Claims.ClaimAccountSchema).FirstOrDefault();

            var tokenClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, userId),
                new Claim(JwtRegisteredClaimNames.Sub, userEmail),
                new Claim(Authorize.Claims.ClaimIp, remoteIpAddress),
                new Claim(Authorize.Claims.ClaimPersonId, personId.ToString()),
                new Claim(Authorize.Claims.ClaimCurrentSchema, ((int)Authorize.AccountLevel.customer).ToString()),
                new Claim(Authorize.Claims.ClaimAccountSchema, accountSchema.ClaimValue)
            };

            var roleClaims = GetAsCustomerRoleClaims(
                employerId,
                userClaims);

            if (roleClaims.Count > 0)
                tokenClaims.AddRange(roleClaims);

            return tokenClaims;
        }

        private static IEnumerable<Claim> GetTokenClaimsConfirmEmail(
            string remoteIpAddress,
            AppUser user,
            string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = ClientHost + returnUrl;

            var climes = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.ConfirmEmail),
                new Claim(Authorize.Claims.ClaimIp, remoteIpAddress),
                new Claim(Authorize.Claims.ClaimPersonId, user.PersonId.GetValueOrDefault().ToString()),
                new Claim(SystemMessage.ReturnUrl, returnUrl),
            };
            return climes;
        }

        private static IEnumerable<Claim> GetTokenClaimsResetPassword(
            string remoteIpAddress,
            AppUser user,
            string resetPassToken)
        {
            var climes = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.ResetPass),
                new Claim(Authorize.Claims.ClaimIp, remoteIpAddress),
                new Claim(Authorize.Claims.ClaimPersonId, user.PersonId.GetValueOrDefault().ToString()),
                new Claim(SystemMessage.ResetPassToken, resetPassToken)
            };
            return climes;
        }

        private static IEnumerable<Claim> GetTokenClaimsResetEmail(
            string remoteIpAddress,
            AppUser user,
            string newEmail,
            string resetEmailToken)
        {
            var climes = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.ResetEmail),
                new Claim(Authorize.Claims.ClaimIp, remoteIpAddress),
                new Claim(Authorize.Claims.ClaimPersonId, user.PersonId.GetValueOrDefault().ToString()),
                new Claim(SystemMessage.ResetEmailToken, resetEmailToken),
                new Claim(SystemMessage.NewEmail, newEmail)
            };
            return climes;
        }

        private static IEnumerable<Claim> GetTokenNoteHubClaims(
            string eventName)
        {
            var climes = new List<Claim>
            {
                new Claim(SystemMessage.RemoteNoteHubRequest, SystemMessage.RemoteNoteHubRequest),
                new Claim(SystemMessage.RemoteEventData, eventName)
            };
            return climes;
        }

        private static List<Claim> GetRoleClaims(
            AppUser user,
            Authorize.AccountLevel requestedLevel)
        {
            var result = new List<Claim>();

            switch (requestedLevel)
            {
                case Authorize.AccountLevel.none:
                    break;
                case Authorize.AccountLevel.admin:
                    if (user.UserRoles.Select(x => x.Role.Name).Contains(Authorize.Roles.SuperAdmin))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.SuperAdmin));
                    break;
                case Authorize.AccountLevel.company:
                    var defaulAccountId = user.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultAccountId)
                        .Select(y => y.ClaimValue).First();
                    var accountClaimRoles = user.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimAccountId && x.ClaimValue == defaulAccountId)
                        .First().UserClaimRoles.Select(z => z.Role.Name).ToList();
                    if (accountClaimRoles.Contains(Authorize.Roles.CompanyOwner))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CompanyOwner));
                    if (accountClaimRoles.Contains(Authorize.Roles.CompanyAdmin))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CompanyAdmin));
                    if (accountClaimRoles.Contains(Authorize.Roles.CompanyManager))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CompanyManager));
                    result.Add(new Claim(Authorize.Claims.ClaimAccountId, defaulAccountId));
                    break;
                case Authorize.AccountLevel.customer:
                    var defaulEmployerId_1 = user.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultEmployerId)
                        .Select(y => y.ClaimValue).First();
                    var employerClaimRoles = user.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimEmployerId && x.ClaimValue == defaulEmployerId_1)
                        .First().UserClaimRoles.Select(z => z.Role.Name).ToList();
                    if (user.UserRoles.Select(x => x.Role.Name).Contains(Authorize.Roles.CustomerOwner))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CustomerOwner));
                    if (employerClaimRoles.Contains(Authorize.Roles.CustomerAdmin))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CustomerAdmin));
                    if (employerClaimRoles.Contains(Authorize.Roles.CustomerAdminManager))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CustomerAdminManager));
                    if (employerClaimRoles.Contains(Authorize.Roles.CustomerManager))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CustomerManager));
                    result.Add(new Claim(Authorize.Claims.ClaimEmployerId, defaulEmployerId_1));
                    break;
                case Authorize.AccountLevel.employee:
                    var defaulEmployerId_2 = user.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimDefaultEmployerId)
                        .Select(y => y.ClaimValue).First();
                    var employerClaimRoles_2 = user.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimEmployerId && x.ClaimValue == defaulEmployerId_2)
                        .First().UserClaimRoles.Select(z => z.Role.Name).ToList();
                    if (employerClaimRoles_2.Contains(Authorize.Roles.Employee))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.Employee));
                    result.Add(new Claim(Authorize.Claims.ClaimEmployerId, defaulEmployerId_2));
                    break;
                case Authorize.AccountLevel.jobseeker:
                    if (user.UserRoles.Select(x => x.Role.Name).Contains(Authorize.Roles.JobSeeker))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.JobSeeker));
                    break;
            }

            return result;
        }

        private static List<Claim> GetRoleClaimsWithCompanyOrEmployer(
            AppUser user,
            string accountId,
            string employerId,
            Authorize.AccountLevel requestedLevel)
        {
            var result = new List<Claim>();

            switch (requestedLevel)
            {
                case Authorize.AccountLevel.none:
                    break;
                case Authorize.AccountLevel.company:
                    var accountClaimRoles = user.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimAccountId && x.ClaimValue == accountId)
                        .First().UserClaimRoles.Select(z => z.Role.Name).ToList();
                    if (accountClaimRoles.Contains(Authorize.Roles.CompanyOwner))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CompanyOwner));
                    if (accountClaimRoles.Contains(Authorize.Roles.CompanyAdmin))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CompanyAdmin));
                    if (accountClaimRoles.Contains(Authorize.Roles.CompanyManager))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CompanyManager));
                    result.Add(new Claim(Authorize.Claims.ClaimAccountId, accountId));
                    break;
                case Authorize.AccountLevel.customer:
                    var employerClaimRoles = user.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimEmployerId && x.ClaimValue == employerId)
                        .First().UserClaimRoles.Select(z => z.Role.Name).ToList();
                    if (user.UserRoles.Select(x => x.Role.Name).Contains(Authorize.Roles.CustomerOwner))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CustomerOwner));
                    if (employerClaimRoles.Contains(Authorize.Roles.CustomerAdmin))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CustomerAdmin));
                    if (employerClaimRoles.Contains(Authorize.Roles.CustomerAdminManager))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CustomerAdminManager));
                    if (employerClaimRoles.Contains(Authorize.Roles.CustomerManager))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CustomerManager));
                    result.Add(new Claim(Authorize.Claims.ClaimEmployerId, employerId));
                    break;
                case Authorize.AccountLevel.employee:
                    var employerClaimRoles_1 = user.Claims.Where(x => x.ClaimType == Authorize.Claims.ClaimEmployerId && x.ClaimValue == employerId)
                        .First().UserClaimRoles.Select(z => z.Role.Name).ToList();
                    if (employerClaimRoles_1.Contains(Authorize.Roles.Employee))
                        result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.Employee));
                    result.Add(new Claim(Authorize.Claims.ClaimEmployerId, employerId));
                    break;
            }

            return result;
        }

        private static List<Claim> GetAsCustomerRoleClaims(
            int employerId,
            IList<AppUserClaim> claims)
        {
            var result = new List<Claim>();
            result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CustomerOwner));
            result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CustomerAdmin));
            result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CustomerAdminManager));
            result.Add(new Claim(Authorize.Claims.ClaimRole, Authorize.Roles.CustomerManager));
            var accountId = claims.Where(x => x.ClaimType == Authorize.Claims.ClaimAccountId).First().ClaimValue;
            result.Add(new Claim(Authorize.Claims.ClaimAccountId, accountId));
            result.Add(new Claim(Authorize.Claims.ClaimEmployerId, employerId.ToString()));

            return result;
        }
    }
}
