using Dap1U.Models;
using System;
using System.ComponentModel.DataAnnotations;
using _1U_ASP.Repositorys;

namespace _1U_ASP.Models
{
    public class SalePriseDoc : BaseEntity
    {
        [Key]
        public int SalePriseDocId { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public decimal SalePrise { get; set; }
        public DateTime DateFrom { get; set; }
        public bool Deleted { get; set; }
        public int? UserActionId { get; set; }
    }
}
