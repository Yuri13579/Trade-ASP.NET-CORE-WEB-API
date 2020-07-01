using System.Collections.Generic;
using System.Threading.Tasks;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace _1U_ASP.Service.Interface
{
    public interface IProviderService
    {
        Task<ActionResult<IEnumerable<Provider>>> GetProviders();
        Task<ActionResult<Provider>> GetProvider(int id);
        Task<DataServiceMessage> PutProvider(Provider provider);
        Task<DataServiceMessage> PostProvider(Provider provider);
        Task<DataServiceMessage> DeleteProvider(int id);
    }
}
