using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using _1U_ASP.Const;
using _1U_ASP.Context;
using _1U_ASP.DTO;
using _1U_ASP.Repositorys.Interface;
using _1U_ASP.Security;
using _1U_ASP.Service.Interface;
using Dap1U.Models;

namespace _1U_ASP.Service.Impl
{
    public class ProductService: IProductService
    {
        
        private readonly IRepository<Product> _product;
        private readonly ApplicationContext _applicationContext;

        public ProductService(
            IRepository<Product> product,
            ApplicationContext applicationContext
        )
        {
            _product = product;
            _applicationContext = applicationContext;
        }
            
        public async Task<Product> GetProductById(int id)
        {
           return await _product.GetByIdAsync(id);
        }

        public async Task<DataServiceMessage> GetAllProducts(JwtSecurityToken tokenJwt)
        {
            var roles = GlobalMethods.GetRolesFromJwtSecurityToken(tokenJwt);
            // var products = _applicationContext.Products.FromSql("spGetAllProducts").ToList();
           
            if (roles.Contains(Authorize.Roles.CompanyOwner)
            || roles.Contains(Authorize.Roles.CompanyAdmin)
            || roles.Contains(Authorize.Roles.CompanyManager)
            ) { 
                var res= await _product.ListAllAsync();
                return new DataServiceMessage
                {
                    Result = true,
                    Data = res,
                    MainMessage = GoodResponses.Ok
                };
            }

            else
            {
                return new DataServiceMessage
                {
                    Result =false,
                    MainMessage = BadResponses.AccessIsDenied
                };
            }
        }

        public async Task<DataServiceMessage> AddProduct(Product product)
        {
            await _product.AddAsync(product);
            return new DataServiceMessage
            {
                Result = true,
                MainMessage = GoodResponses.AddedSuccessfully
            };
        }

        public async Task<DataServiceMessage> DeleteProductById(int id)
        {
            await _product.DeleteAsyncById(id);
            return new DataServiceMessage
            {
                Result = true,
                MainMessage = GoodResponses.DeletedSuccessfully
            };
        }

        public async Task<DataServiceMessage> PutProduct(Product product)
        {
           var old = await _product.GetByIdAsync(product.ProductId);
           if (old != null &&
                (string.Equals(old.Name, product.Name, StringComparison.CurrentCultureIgnoreCase) == false 
                || string.Equals(old.Description, product.Description, StringComparison.CurrentCultureIgnoreCase) == false
                || old.Barcode.Equals(product.Barcode) == false
                || old.PriceCost.Equals(product.PriceCost) == false
                || old.PriseSale.Equals(product.PriseSale) == false
               ))
            {
                old.Name = product.Name;
                old.Description= product.Description;
                old.Barcode = product.Barcode;
                old.PriceCost = product.PriceCost;
                old.PriseSale = product.PriseSale;
                await _product.UpdateAsync(old);
                return new DataServiceMessage
                {
                    Result = true,
                    MainMessage = GoodResponses.UpdatedSuccessfully
                };
            }

            if (old == null)
            {
                product.ProductId = 0;
                await AddProduct(product);
            }
            
            return new DataServiceMessage
            {
                Result = true,
                MainMessage = GoodResponses.UpdatedSuccessfully
            };
        }
        
    }
}
