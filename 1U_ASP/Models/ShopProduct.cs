using _1U_ASP.Repositorys;
using Dap1U.Models;

namespace _1U_ASP.Models
{
    public class ShopProduct : BaseEntity
    {
        public int ShopProductId { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool Deleted { get; set; }
        public int? UserActionId { get; set; }


    }
}
