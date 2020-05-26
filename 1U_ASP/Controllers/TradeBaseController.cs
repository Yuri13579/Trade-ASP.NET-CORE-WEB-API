using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Const;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace _1U_ASP.Controllers
{
    //[Route(ControllerAttribute.AWWCorDefaultRoute)]
    //[Produces(ControllerAttribute.ApplicationJson)]
    //[EnableCors(ControllerAttribute.PolicyName)]
    public class TradeBaseController : Controller
    {
        protected BadRequestObjectResult SetInvalidModelResponse()
        {
            HttpContext.Response.StatusCode = 400;
            return BadRequest(string.Join(Environment.NewLine, ModelState.Values.SelectMany(
                x => x.Errors).Select(x => x.ErrorMessage)));
        }

        protected string GetEmailFromTokenJwt()
        {
            return GetTokenJwt().Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        }

        protected string GetUserIdFromTokenJwt()
        {
            return GetTokenJwt().Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;
        }

        protected JwtSecurityToken GetTokenJwt()
        {
            var tokenJwt = new JwtSecurityTokenHandler().ReadToken(HttpContext.Request
                .Headers[Authorize.HttpContextRequest.HeadersAuthorization]
                .ToString().Substring(7)) as JwtSecurityToken;
            return tokenJwt;
        }

        protected int GetUserActionId()
        {
            return (int)HttpContext.Items[Authorize.Tokens.UserActionId];
        }
    }

}
