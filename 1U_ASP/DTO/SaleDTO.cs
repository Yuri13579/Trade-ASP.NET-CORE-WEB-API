using System;

namespace _1U_ASP.DTO
{
    public class SaleDTO
    {
        public int SaleOrderID { get; set; }
        public DateTime DataTime { get; set; }
        public int SaleOrderDetailId { get; set; }
        public int ProductId { get; set; }
        public double ProductBarcode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Count { get; set; }
        public double PriceCost { get; set; }
        public double PriseSale { get; set; }
        public double Summ { get; set; }
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string DisplayData { get; set; }
        public DateTime Sort { get; set; }
    }
}
