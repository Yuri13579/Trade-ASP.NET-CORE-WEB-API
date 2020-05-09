using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Repositorys;

namespace _1U_ASP.Models
{
    public class Provider : BaseEntity
    {
        [Key]
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public decimal Phone { get; set; }
        public string Address { get; set; }
        public bool Deleted { get; set; }
        public int? UserActionId { get; set; }

        public ICollection<DocEnterProduct> DocEnterProducts { get; set; }
        
    }
}
