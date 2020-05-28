using System.Collections.Generic;

namespace _1U_ASP.DTO
{
    public class ListSellDto
    {
        public List<SellDto> SellDtos { get; set; }
    }
    
    public class SellDto
    {
        public int ProductId { get; set; }
      //  public double ProductBarcode { get; set; }
       // public string ProductName { get; set; }
        public int Count { get; set; }
       // public double PriceCost { get; set; }
        //public double PriseSale { get; set; }
       // public double Summ { get; set; }
        public int ShopId { get; set; }
    }
    
}
