using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _1U_ASP.Context;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using _1U_ASP.Service.Interface;

namespace _1U_ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(
            IPersonService personService
            )
        {
            _personService = personService;
        }

        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _personService.GetPerson();
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            return await _personService.GetPerson( id);
        }

        // PUT: api/Person/5
        [HttpPut("{PutPerson}")]
        public async Task<DataServiceMessage> PutPerson([FromBody] Person person)
        {
            return await _personService.PutPerson(person);
            
        }

        // POST: api/Person
        [HttpPost]
        public async Task<DataServiceMessage> PostPerson(Person person)
        {
            return await _personService.PostPerson(person);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<DataServiceMessage> DeletePerson(int id)
        {
            return await _personService.DeletePerson( id);
            
        }
        
    }
}
