using System.Collections.Generic;
using System.Threading.Tasks;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace _1U_ASP.Service.Interface
{
    public interface IPersonService
    {
        Task<ActionResult<IEnumerable<Person>>> GetPerson();
        Task<ActionResult<Person>> GetPerson(int id);
        Task<DataServiceMessage> PutPerson(Person person);
        Task<DataServiceMessage> PostPerson(Person person);
        Task<DataServiceMessage> DeletePerson(int id);
    }
}
