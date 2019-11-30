using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.Models
{
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }
        public string NameProvider { get; set; }
        public decimal Phone { get; set; }
        public string Address { get; set; }

        public ICollection<DocEnterProduct> DocEnterProducts { get; set; }
        
    }
}
