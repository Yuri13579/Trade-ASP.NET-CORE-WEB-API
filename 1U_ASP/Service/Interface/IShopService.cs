using System.Collections.Generic;
using System.Threading.Tasks;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace _1U_ASP.Service.Interface
{
    public interface IShopService
    {
        Task<ActionResult<IEnumerable<Shop>>> GetAllShops();
        Task<ActionResult<Shop>> GetShop(int id);
        Task<DataServiceMessage> PutShop(Shop shop);
        Task<DataServiceMessage> PostShop(Shop shop);
        Task<DataServiceMessage> DeleteShop(int id);
    }
}
