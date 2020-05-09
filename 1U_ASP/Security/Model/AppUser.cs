using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace _1U_ASP.Security.Model
{
    public class AppUser : IdentityUser
    {
        public int? PersonId { get; set; }

        public virtual ICollection<AppUserClaim> Claims { get; set; }
        public virtual ICollection<AppUserLogin> Logins { get; set; }
        public virtual ICollection<AppUserToken> Tokens { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual AppPerson Person { get; set; }
    }

    public class AppRole : IdentityRole
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<AppRoleClaim> RoleClaims { get; set; }
        public virtual ICollection<AspNetUserClaimRole> UserClaimRoles { get; set; }
    }

    public class AppUserRole : IdentityUserRole<string>
    {
        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }

    public class AppUserClaim : IdentityUserClaim<string>
    {
        public string ClaimProperty { get; set; }

        public virtual AppUser User { get; set; }
        public virtual ICollection<AspNetUserClaimRole> UserClaimRoles { get; set; }
    }

    public class AppUserLogin : IdentityUserLogin<string>
    {
        public virtual AppUser User { get; set; }
    }

    public class AppRoleClaim : IdentityRoleClaim<string>
    {
        public virtual AppRole Role { get; set; }
    }

    public class AppUserToken : IdentityUserToken<string>
    {
        public virtual AppUser User { get; set; }
    }

    public class AspNetUserClaimRole
    {
        public int AspNetUserClaimRoleId { get; set; }
        public int ClaimId { get; set; }
        public string RoleId { get; set; }

        public virtual AppUserClaim UserClaim { get; set; }
        public virtual AppRole Role { get; set; }
    }

    public class AppPerson
    {
        public AppPerson()
        {
            AspNetUsers = new HashSet<AppUser>();
        }
        [Key]
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public int? GenderSysCodeUniqueId { get; set; }
        public DateTime? Dob { get; set; }
        public int? UserActionId { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<AppUser> AspNetUsers { get; set; }
    }
}
