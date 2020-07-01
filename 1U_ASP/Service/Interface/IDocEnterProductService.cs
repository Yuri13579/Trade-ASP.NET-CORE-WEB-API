using System.Collections.Generic;
using System.Threading.Tasks;
using _1U_ASP.DTO;

namespace _1U_ASP.Service.Interface
{
    public interface IDocEnterProductService
    {
        Task<List<DocEnterProductDto>> GetAllEnterProducts();
        Task<List<DocEnterProductDetailDto>> GetDocEnterProductDetailsById(int docId);
    }
}
