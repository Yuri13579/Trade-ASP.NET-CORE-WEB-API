using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public double Barcode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PriseSale { get; set; }
    }
}
