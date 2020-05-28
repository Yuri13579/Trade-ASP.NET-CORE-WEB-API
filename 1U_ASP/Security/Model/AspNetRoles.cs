using System;

namespace _1U_ASP.Security.Model
{
    public class AspNetRoles : IEquatable<AspNetRoles>
    {
        public string Id { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        
        public bool Equals(AspNetRoles obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                AspNetRoles p = (AspNetRoles)obj;
                return (Id.Equals(p.Id)) 
                       && (ConcurrencyStamp.Equals(p.ConcurrencyStamp))
                       && (Name.Equals(p.Name))
                       && (NormalizedName.Equals(p.NormalizedName));
            }
        }
    }
}
