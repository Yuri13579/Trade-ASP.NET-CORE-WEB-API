using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Const;
using _1U_ASP.Context;
using _1U_ASP.DTO;
using _1U_ASP.Models;
using _1U_ASP.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _1U_ASP.Service.Impl
{
    public class PersonService : IPersonService
    {
        private readonly ApplicationContext _context;
      
        public PersonService(ApplicationContext context
            
        )
        {
            _context = context;
        }
        
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _context.Person.ToListAsync();
        }

        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.Person.FindAsync(id);

            return person;
        }

        public async Task<DataServiceMessage> PostPerson(Person person)
        {
            try
            {
                _context.Person.Add(person);
            await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new DataServiceMessage
                {
                    Result =false,
                    MainMessage = e.Message
                };
            }

            return new DataServiceMessage
            {
                Result = true,
                MainMessage = GoodResponses.AddedSuccessfully
            }; 
        }

        public async Task<DataServiceMessage> PutPerson(Person person)
        {
            try
            {
                _context.Entry(person).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new DataServiceMessage
                {
                    Result = false,
                    MainMessage = e.Message
                };
            }
            return new DataServiceMessage
            {
                Result = true,
                MainMessage = GoodResponses.UpdatedSuccessfully
            };

        }

        public async Task<DataServiceMessage> DeletePerson(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            { 
                return new DataServiceMessage
                {
                    Result = false,
                    MainMessage = BadResponses.UserIsnTFound
                };
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return new DataServiceMessage
            {
                Result = true,
                MainMessage = GoodResponses.DeletedSuccessfully
            };
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.PersonId == id);
        }
    }
}
