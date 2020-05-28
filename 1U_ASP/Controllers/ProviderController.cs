using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _1U_ASP.DTO;
using _1U_ASP.Service.Interface;
using _1U_ASP.Models;
using _1U_ASP.Security.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace _1U_ASP.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[TokenFilter]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(
            IProviderService providerService
            )
        {
           _providerService = providerService;
        }

       // [AllowAnonymous]
        // [Authorize(Roles = Authorize.Roles.CompanyOwnerCompanyAdminCompanyManager)]
        // GET: api/Provider
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> GetProviders()
        {
            return await _providerService.GetProviders();
           
        }

        // GET: api/Provider/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProvider(int id)
        {
            var provider = await _providerService.GetProvider(id);
            if (provider == null)
            {
                return NotFound();
            }

            return provider;
        }

        // PUT: api/Provider/5
        [HttpPut("{PutProvider}")]
        public async Task<DataServiceMessage> PutProvider( Provider provider)
        {
            return await _providerService.PutProvider(provider);
           
        }

        // POST: api/Provider
        [HttpPost("{PostProvider}")]
        public async Task<string> PostProvider(Provider provider)
        {
            return await _providerService.PostProvider(provider);
          
        }

        // DELETE: api/Provider/5
        [HttpDelete("{DeleteProvider}/{id}")]
        public async Task<string> DeleteProvider(int id)
        {
            return await _providerService.DeleteProvider(id);
        }
    }
}
