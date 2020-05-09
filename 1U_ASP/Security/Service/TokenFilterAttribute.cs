using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.Const;
using _1U_ASP.Security.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace _1U_ASP.Security.Service
{
    public class TokenFilterAttribute : TypeFilterAttribute
    {
        public TokenFilterAttribute() : base(typeof(ContextActionFilterAttribute))
        {
        }

        private class ContextActionFilterAttribute : Attribute, IAsyncActionFilter
        {
            private readonly ILogActionServeProcess _logActionServeProcess;

            public ContextActionFilterAttribute(ILogActionServeProcess logActionServeProcess)
            {
                _logActionServeProcess = logActionServeProcess;
            }

            public async Task OnActionExecutionAsync(
                ActionExecutingContext context,
                ActionExecutionDelegate next)
            {
                LogActionDto logActionDto = null;
                string tokenRequest = string.Empty;

                try
                {
                    var remoteIpAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
                    var webClickUrl = context.HttpContext.Request.Path.Value;
                    if (webClickUrl.Length > 200)
                        webClickUrl = webClickUrl.Substring(0, 200);
                    var actionSysCodeName = context.HttpContext.Request.Method;
                    if (actionSysCodeName.Length > 50)
                        actionSysCodeName = actionSysCodeName.Substring(0, 50);
                    var browser = context.HttpContext.Request.Headers[Authorize.HttpContextRequest.UserAgent].ToString();
                    if (browser.Length > 50)
                        browser = browser.Substring(0, 50);

                    logActionDto = new LogActionDto
                    {
                        Ipaddress = remoteIpAddress,
                        WebClickUrl = webClickUrl,
                        Method = actionSysCodeName,
                        Browser = browser
                    };

                    var url = context.HttpContext.Request.Headers[Authorize.Tokens.Url].ToString();

                    if (!string.IsNullOrEmpty(url))
                    {
                        if (url.Length > 200)
                            url = url.Substring(0, 200);

                        logActionDto.WebClickUrl = url;
                    }

                    if (context.HttpContext.Request.Headers.ContainsKey(Authorize.HttpContextRequest.HeadersAuthorization))
                    {
                        if (StringValues.IsNullOrEmpty(
                            context.HttpContext.Request.Headers[Authorize.HttpContextRequest.HeadersAuthorization]))
                        {
                            var userId = Guid.NewGuid().ToString();
                            logActionDto.UserId = Authorize.Roles.Guest;
                        }
                        else
                        {
                            tokenRequest = context.HttpContext.Request
                                .Headers[Authorize.HttpContextRequest.HeadersAuthorization].ToString()
                                .Substring(7);
                            var tokenJwt = new JwtSecurityTokenHandler().ReadToken(tokenRequest) as JwtSecurityToken;

                            if (tokenJwt != null)
                            {
                                var ip = tokenJwt.Claims.FirstOrDefault(x => x.Type == Authorize.Claims.ClaimIp)?.Value;
                                var role = tokenJwt.Claims.FirstOrDefault(x => x.Type == Authorize.Claims.ClaimRole)?.Value;

                                if (ip != remoteIpAddress)
                                    if (role != Authorize.Roles.ResetPass && role != Authorize.Roles.ConfirmEmail && role != Authorize.Roles.ResetEmail)
                                    {
                                        context.Result = new UnauthorizedResult();
                                        return;
                                    }
                            }
                            if (tokenJwt != null)
                            {
                                logActionDto.UserId = tokenJwt.Id;
                                var claimPersonId = tokenJwt.Claims.Where(x => x.Type == Authorize.Claims.ClaimPersonId).FirstOrDefault();
                                if (claimPersonId != null)
                                    logActionDto.PersonId = Convert.ToInt32(claimPersonId.Value);
                            }
                        }
                    }
                    else logActionDto.UserId = Authorize.Roles.Guest;

                    if (actionSysCodeName == SystemMessage.Post
                        || actionSysCodeName == SystemMessage.Put
                        || actionSysCodeName == SystemMessage.Delete)
                    {
                        var userActionId = await _logActionServeProcess.CreateAppAction(logActionDto);
                        context.HttpContext.Items.Add(Authorize.Tokens.UserActionId, userActionId);
                    }

                    await next();
                }
                catch (Exception ex)
                {
                    var data = ExceptionHandler.SerializeForException(logActionDto) + " TOKEN: " + tokenRequest;
                    throw ExceptionHandler.MyCustomException(ex, data);
                }
            }
        }
    }
}
