using Dap1U.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using _1U_ASP.DTO;

namespace _1U_ASP.Service.Interface
{
    public interface IProductService
    {
        Task<Product> GetProductById(int id);
        Task<DataServiceMessage>  GetAllProducts(JwtSecurityToken tokenJwt);

        Task<bool> AddProduct(Product productDto);
        Task<bool> DeleteProductById(int id);
        Task<bool> PutProduct(Product product);
    }
}
