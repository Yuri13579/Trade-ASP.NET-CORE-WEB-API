using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using _1U_ASP.Repositorys.Interface;
using _1U_ASP.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace _1U_ASP.Service.Impl
{
    public class DocEnterProductService : IDocEnterProductService
    {
        private readonly IRepository<DocEnterProduct> _docEnterProduct;
        private readonly IRepository<DocEnterProductDetail> _docEnterProductDetail;
        
        public DocEnterProductService(
            IRepository<DocEnterProduct> docEnterProduct,
            IRepository<DocEnterProductDetail> docEnterProductDetail
            )
        {
            _docEnterProduct = docEnterProduct;
            _docEnterProductDetail = docEnterProductDetail;
        }
        
        public async Task<List<DocEnterProductDetailDto>> GetAllEnterProductDetails()
        {
            return await GetEnterProductDetails();
        }
        
        private async Task<List<DocEnterProductDetailDto>> GetEnterProductDetails()
        {
            var result = await (from docEnterProduct in _docEnterProduct.List(x => x.Deleted == false)
                    join docEnterProductDetail in _docEnterProductDetail.List(x => x.Deleted == false)
                        on docEnterProduct.DocEnterProductId equals docEnterProductDetail.DocEnterProductId
                        select new DocEnterProductDetailDto
                        {
                            Count = docEnterProductDetail.Count,
                            DocEnterProductDetailId = docEnterProductDetail.DocEnterProductDetailId,
                            DocEnterProductId = docEnterProduct.DocEnterProductId,
                            InPrise = docEnterProductDetail.InPrise,
                            ProductId = docEnterProductDetail.ProductId,
                            ProviderId = docEnterProduct.ProviderId,
                            Summ = docEnterProductDetail.Summ
                    }
                ).ToListAsync();
            return result;
        }




    }
}
