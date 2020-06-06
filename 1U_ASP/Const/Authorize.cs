using System;

namespace _1U_ASP.Const
{
    public static class Authorize
    {
        [Flags]
        public enum AccountLevel
        {
            none = 0,
            admin = 1,
            company = 2,
            customer = 4,
            employee = 8,
            jobseeker = 16
        }

        public class Roles
        {
           // public const string JobSeeker = "jobseeker";
            public const string Employee = "employee";
            public const string CompanyOwner = "companyowner";
            public const string CompanyAdmin = "companyadmin";
            public const string CompanyManager = "companymanager";
            //public const string CustomerOwner = "customerowner";
            //public const string CustomerAdmin = "customeradmin";
            //public const string CustomerAdminManager = "customeradminmanager";
            //public const string CustomerManager = "customermanager";
            //public const string SuperAdmin = "superadmin";

            public const string Guest = "guest";

            public const string ResetEmail = "resetemail";
            public const string ResetPass = "resetpass";
            public const string ConfirmEmail = "confirmemail";

            public const string CompanyOwnerCompanyAdminCompanyManager =
                "companyowner, companyadmin, companymanager";
            public const string CompanyOwnerCompanyAdminCustomerOwnerCustomerAdmin =
                "companyowner, companyadmin, customerowner, customeradmin";
            public const string CompanyOwnerCompanyAdminCustomerOwnerCustomerAdminCustomerAdminManager =
                "companyowner, companyadmin, customerowner, customeradmin, customeradminmanager";
            public const string JobSeekerCompanyOwnerCompanyAdminCompanyManager =
                "jobseeker, companyowner, companyadmin, companymanager";
            //public const string CustomerOwnerCustomerAdminCustomerAdminManager =
            //    "customerowner, customeradmin, customeradminmanager";
            //public const string CustomerOwnerCustomerAdmin =
            //    "customerowner, customeradmin";
            //public const string CustomerOwnerCustomerManager =
            //    "customerowner, customermanager";
            //public const string CompanyOwnerCompanyAdmin =
            //    "companyowner, companyadmin";
            public const string CompanyAdminCompanyManager =
                "companyadmin, companymanager";
            public const string CompanyOwnerCustomerOwner =
                "companyowner, customerowner";

            public const string GoogleCalendarApplicationScheme = "Application";
            public const string GoogleCalendarCalendarId2 = "primary";
            public const string TrelloAppKey = "b6ffe9d426bca6353f61d73ccecbd872";
        }

        public class Claims
        {
            public const string ClaimAccountSchema = "accountschema";
            public const string ClaimDefaultSchema = "defaultschema";

            public const string ClaimAccountId = "accountId";
            public const string ClaimDefaultAccountId = "defaultaccountId";
            public const string ClaimEmployerId = "employerId";
            public const string ClaimDefaultEmployerId = "defaultemployerId";

            public const string ClaimCurrentSchema = "currentschema";
            public const string CurrentAccountId = "currentaccountId";
            public const string CurrentEmployerId = "currentemployerId";

            public const string ClaimPersonId = "PersonId";
            public const string ClaimIp = "ip";
            public const string ClaimRole = "role";
        }

        public class Tokens
        {
            public const string Token = "token";
            public const string AccessControlExposeHeaders = "Access-Control-Expose-Headers";
            public const string ContentLengthToken = "Content-Length, token";
            public const string TimeReport = "timereport";

            public const string Url = "url";
            public const string UserActionId = "userActionId";

            public const string FirstName = "first_name";
            public const string LastName = "last_name";
            public const string EmailSmall = "email";
            public const string Elements = "elements";
            public const string Handle = "handle~";
            public const string EmailAddress = "emailAddress";
            public const string LocalizedLastName = "localizedLastName";
            public const string LocalizedFirstName = "localizedFirstName";
            public const string GivenName = "given_name";
            public const string FamilyName = "family_name";
            public const string Name = "name";
            public const string Bearer = "Bearer";
            public const string Id = "id";
            public const string Sub = "sub";
            public const string AccessToken = "access_token";

            public const string Facebook = "Facebook";
            public const string Linkedin = "Linkedin";
            public const string Google = "Google";

            public const string FaceBookScopeFields = "fields=id,name,first_name,last_name,email";
        }

        public class HttpContextRequest
        {
            public const string HeadersAuthorization = "Authorization";
            public const string UserAgent = "User-Agent";
            public const string Origin = "Origin";
        }
    }
}
