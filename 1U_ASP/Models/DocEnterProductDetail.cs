using System.ComponentModel.DataAnnotations;
using _1U_ASP.Repositorys;
using Dap1U.Models;

namespace _1U_ASP.Models
{
    public class DocEnterProductDetail : BaseEntity
    {
        [Key]
        public int DocEnterProductDetailId { get; set; }
        public int DocEnterProductId { get; set; }
        public DocEnterProduct DocEnterProduct { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int? ShopProductId { get; set; }
        public ShopProduct ShopProduct { get; set; }
        public int Count { get; set; }
        public float InPrise { get; set; }
        public double Summ { get; set; }
        public bool Deleted { get; set; }
        public int UserActionId { get; set; }
        
    }
}
