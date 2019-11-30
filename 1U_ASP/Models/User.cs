using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _1U_ASP.Models
{
    public class User: IdentityUser
    { 
        public int ProfileId { get; set; }

     //   public virtual Profile Profile { get; set; }
        
    }
}
