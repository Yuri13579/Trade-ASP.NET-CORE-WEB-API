using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using _1U_ASP.Const;

namespace _1U_ASP.Security
{
    public static class GlobalMethods
    {
        public static int GetAccountIdFromJwtSecurityToken(JwtSecurityToken tokenJwt)
        {
            Int32.TryParse(tokenJwt.Claims.FirstOrDefault(x => x.Type == Authorize.Claims.ClaimAccountId)?.Value, out int result);
            return result;
        }

        public static string GetEMailAddressFromJwtSecurityToken(JwtSecurityToken tokenJwt)
        {
            return tokenJwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        }

        public static string GetUserIdFromJwtSecurityToken(JwtSecurityToken tokenJwt)
        {
            return tokenJwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;
        }

        public static int GetEmployerIdFromJwtSecurityToken(JwtSecurityToken tokenJwt)
        {
            Int32.TryParse(tokenJwt.Claims.FirstOrDefault(x => x.Type == Authorize.Claims.ClaimEmployerId)?.Value, out int result);
            return result;
        }

        public static List<string> GetRolesFromJwtSecurityToken(JwtSecurityToken tokenJwt)
        {
            return tokenJwt.Claims.Where(x => x.Type == Authorize.Claims.ClaimRole).Select(y => y.Value).ToList();
        }

        public static int GetPersonIdFromJwtSecurityToken(JwtSecurityToken tokenJwt)
        {
            Int32.TryParse(tokenJwt.Claims.FirstOrDefault(x => x.Type == Authorize.Claims.ClaimPersonId)?.Value, out int result);
            return result;
        }

        public static int GetCurrentAccountSchemaFromJwtSecurityToken(JwtSecurityToken tokenJwt)
        {
            Int32.TryParse(tokenJwt.Claims.FirstOrDefault(x => x.Type == Authorize.Claims.ClaimCurrentSchema)?.Value, out int result);
            return result;
        }

        public static DateTime GetBeginningDayDateTime(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 1);
        }

        public static DateTime GetEndDayDateTime(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        //public static bool IsDevEnvironments()
        //{
        //    return GlobalVariables.EnvironmentType == Var.Development
        //            || GlobalVariables.EnvironmentType == Var.QA
        //            || GlobalVariables.EnvironmentType == Var.LocalIK;
        //}
    }
}
