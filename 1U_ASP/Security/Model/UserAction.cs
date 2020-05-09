using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Repositorys;

namespace _1U_ASP.Security.Model
{
    public class UserAction : BaseEntity
    {
        public int UserActionId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int ActionSysCodeUniqueId { get; set; }
        public string WebClickUrl { get; set; }
        public DateTime ActionDate { get; set; }
        public string Browser { get; set; }
        public string Device { get; set; }
        public string Os { get; set; }
        public string Ipaddress { get; set; }
        public string Country { get; set; }
        public bool Deleted { get; set; }
    }
}
