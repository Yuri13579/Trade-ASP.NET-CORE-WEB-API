using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace _1U_ASP.Context
{
    public class GlobalVariables
    {
        public const string ZeroStringValueAsFalseOrNo = "0";
        public const string OneStringValueAsTrueOrYes = "1";
        public static string ConnectionStringMainDatabase { get; set; }
        public static string ApiKey { get; set; }
        public static string BillingPeriodTemplateBase { get; set; }
        public static string BillingPeriodTemplateForNew { get; set; }
        public static string CustomBillingPeriodTemplate { get; set; }
        public static string ExceptionTemplate { get; set; }
        public static string InviteAccountManager { get; set; }
        public static string ApplicationLetter { get; set; }
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static SigningCredentials SigningCredentials { get; set; }
        public static string SecretKey { get; set; }
        public static string EnvironmentType { get; set; }
        public static string ClientHost { get; set; }
        public static string MainServerUrl { get; set; }
        public static string SecurityServerUrl { get; set; }
        public static string SignalRConnectionString { get; set; }
    }
}
