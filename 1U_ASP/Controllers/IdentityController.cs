using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.MiddleTier;
using _1U_ASP.DTO;
using _1U_ASP.MiddleTier.Interface;

namespace _1U_ASP.Controllers
{

    // [Route("[controller]/[action]")]
    //  [Route("reg")]
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly ILoginServices _loginServices;

        public IdentityController(ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        [HttpGet("Ge/{id}")]
        public async Task<string> Ge(int id)
        {
            return id.ToString();

        }

        // [HttpPost]
        [HttpPost("registration/")]
        public ActionResult<string> Registration([FromBody]RegistrationDTO registrationDto)
        {

            var result = _loginServices.RegisterAsync(registrationDto.Email, registrationDto.Password);

            return result.Result.Result;
        }
    }
}
