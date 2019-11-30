using Dap1U.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.Models
{
    public class SalePriseDoc
    {
        [Key]
        public int SalePriseDocId { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public decimal SalePrise { get; set; }
        public DateTime DateFrom { get; set; }
        public bool DELETED { get; set; }
        public int userActionId { get; set; }
    }
}
