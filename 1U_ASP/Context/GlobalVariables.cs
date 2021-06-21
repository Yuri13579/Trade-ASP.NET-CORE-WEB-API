using Microsoft.IdentityModel.Tokens;

namespace _1U_ASP.Context
{
    public class GlobalVariables
    {
        public static string ConnectionStringMainDatabase { get; set; }
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static SigningCredentials SigningCredentials { get; set; }
        public static string SecretKey { get; set; }
        public static string EnvironmentType { get; set; }
        public static string ClientHost { get; set; }
    }
}
