using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using _1U_ASP.Const;
using _1U_ASP.MiddleTier;
using _1U_ASP.DTO;
using _1U_ASP.MiddleTier.Interface;
using _1U_ASP.Models.Result;
using _1U_ASP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

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

        [Authorize(Roles = "admin")]
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
      //  [Authorize(Roles = "admin")]
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
        

        //private List<Person> people = new List<Person>
        //{
        //    new Person {Login="admin@gmail.com", Password="12345", Role = "admin" },
        //    new Person { Login="qwerty", Password="55555", Role = "user" }
        //};

        //[HttpPost("token1/")]
        //public async Task Token1(LoginViewModel model)
        //{
        //    var username = model.Email; //Request.Form["username"];
        //    var password = model.Password; //Request.Form["password"];

        //    var identity = GetIdentity(username, password);
        //    if (identity == null)
        //    {
        //        Response.StatusCode = 400;
        //        await Response.WriteAsync("Invalid username or password.");
        //        return;
        //    }

        //    var now = DateTime.UtcNow;
        //    // создаем JWT-токен
        //    var jwt = new JwtSecurityToken(
        //            issuer: AuthOptions.ISSUER,
        //            audience: AuthOptions.AUDIENCE,
        //            notBefore: now,
        //            claims: identity.Claims,
        //            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
        //            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        //    var response = new
        //    {
        //        access_token = encodedJwt,
        //        username = identity.Name
        //    };

        //    // сериализация ответа
        //    Response.ContentType = "application/json";
        //    await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        //}

        //private ClaimsIdentity GetIdentity(string username, string password)
        //{
        //    Person person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
        //    if (person != null)
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
        //            new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
        //        };
        //        ClaimsIdentity claimsIdentity =
        //        new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
        //            ClaimsIdentity.DefaultRoleClaimType);
        //        return claimsIdentity;
        //    }

        //    // если пользователя не найдено
        //    return null;
        //}

    }
}
