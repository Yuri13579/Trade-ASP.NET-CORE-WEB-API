using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Const;
using _1U_ASP.DTO;
using _1U_ASP.MiddleTier;
using _1U_ASP.Models;
using _1U_ASP.Security;
using _1U_ASP.Security.Model;
using _1U_ASP.Security.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1U_ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region DI
        private readonly IAccountService _accountService;

        public AccountController(
             IAccountService accountService
        )
        {
           _accountService = accountService;
        }
        #endregion

        [AllowAnonymous]
        [HttpPost("{Register}")]
        public async Task<string> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadResponses.IncorrectInputData;
            }
            try
            {
                model.TrimAllStringsWhithNull();

                var result = await _accountService.Register(
                model);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [AllowAnonymous]
        [HttpPost]//("{Login:}")
        public async Task<LoginResultDto> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new LoginResultDto { Token = BadResponses.IncorrectInputData };
            }

            try
            {
                model.TrimAllStrings();

                var result = await _accountService.Login(
                    HttpContext.Connection.RemoteIpAddress.ToString(),
                    model);
                return new LoginResultDto{Token = result}; 
            }
            catch (Exception ex)
            {
                return new LoginResultDto { Token = ex.Message };
            }
        }


    }
}