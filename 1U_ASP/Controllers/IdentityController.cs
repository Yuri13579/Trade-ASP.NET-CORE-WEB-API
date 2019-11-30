using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Const;
using _1U_ASP.MiddleTier;
using _1U_ASP.DTO;
using _1U_ASP.MiddleTier.Interface;
using _1U_ASP.Models.Result;
using _1U_ASP.Models;

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
        public ActionResult<string> Ge(int id)
        {
            return Json(id.ToString());

        }

        // [HttpPost]
        [HttpPost("registration/")]
        public async Task<IActionResult> Registration([FromBody]RegistrationDTO registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(SysConst.ParameterNoValid);
            }

            var result = await _loginServices.RegisterAsync(registrationDto.Email, registrationDto.Password);

            return Json( result.Result);
        }

        //[HttpGet]
        //public IActionResult Login(string returnUrl = null)
        //{
        //    return View(new LoginViewModel { ReturnUrl = returnUrl });
        //}


        // [ValidateAntiForgeryToken]
        // [HttpPost]
        [HttpPost("login/")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(SysConst.ParameterNoValid);
            }

            var result = await _loginServices.Login(model);
            
                if (result.Succeeded)
                {
                    return Json(SysConst.CorrectLogin);
                }
                else
                {
                    return Json("error Login");
                }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> LogOff()
        //{
        //    // удаляем аутентификационные куки
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}



    }
}
