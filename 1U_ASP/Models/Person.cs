using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Repositorys;

namespace _1U_ASP.Models
{
    public class Person : BaseEntity
    {
        public Person()
        {
            
            Profile = new HashSet<Profile>();
        }

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
        public ICollection<Profile> Profile { get; set; }
    }
}
