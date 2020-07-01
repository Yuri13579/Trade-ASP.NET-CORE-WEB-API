using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using _1U_ASP.Repositorys.Interface;
using _1U_ASP.Service.Interface;
using Dap1U.Models;
using Microsoft.EntityFrameworkCore;

namespace _1U_ASP.Service.Impl
{
    public class DocEnterProductService : IDocEnterProductService
    {
        private readonly IRepository<DocEnterProduct> _docEnterProduct;
        private readonly IRepository<DocEnterProductDetail> _docEnterProductDetail;
        private readonly IRepository<Provider> _provider;
        private readonly IRepository<Product> _product;

        public DocEnterProductService(
            IRepository<DocEnterProduct> docEnterProduct,
            IRepository<DocEnterProductDetail> docEnterProductDetail,
            IRepository<Provider> provider,
            IRepository<Product> product
            )
        {
            _docEnterProduct = docEnterProduct;
            _docEnterProductDetail = docEnterProductDetail;
            _provider = provider;
            _product = product;
        }
        
        public async Task<List<DocEnterProductDto>> GetAllEnterProducts()
        {
            var result = await (from docEnterProduct in _docEnterProduct.List(x => x.Deleted == false)
                    join provider in _provider.List(x => x.Deleted == false)
                        on docEnterProduct.ProviderId equals provider.ProviderId
                    select new DocEnterProductDto
                    {
                        DocEnterProductId = docEnterProduct.DocEnterProductId,
                         ProviderId = provider.ProviderId,
                         ProviderName = provider.Name,
                         DocDate = docEnterProduct.DocDate
                    }
                ).ToListAsync();
            return result;
        }

        public async Task<List<DocEnterProductDetailDto>> GetDocEnterProductDetailsById(int docId)
        {
            var result = await (from docEnterProduct in _docEnterProduct.List(x => x.Deleted == false && x.DocEnterProductId == docId)
                    join docEnterProductDetail in _docEnterProductDetail.List(x => x.Deleted == false)
                        on docEnterProduct.DocEnterProductId equals docEnterProductDetail.DocEnterProductId
                    join product in _product.List(x=> x.Deleted == false)
                        on docEnterProductDetail.ProductId equals product.ProductId 
                    select new DocEnterProductDetailDto
                    {
                        Count = docEnterProductDetail.Count,
                        DocEnterProductDetailId = docEnterProductDetail.DocEnterProductDetailId,
                        DocEnterProductId = docEnterProduct.DocEnterProductId,
                        InPrise = docEnterProductDetail.InPrise,
                        ProductId = docEnterProduct.DocEnterProductId,
                        Summ = docEnterProductDetail.Summ,
                        ProductName = product.Name
                    }
                ).ToListAsync();
            return result;
            
        }
        

    }
}
