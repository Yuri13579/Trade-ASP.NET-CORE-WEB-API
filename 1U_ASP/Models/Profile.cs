using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }

        public string Gender { get; set; }
        public bool Deleted { get; set; }
        public int? UserActionId { get; set; }

        //  public DateTime DOB { get; set; }
    }
}
