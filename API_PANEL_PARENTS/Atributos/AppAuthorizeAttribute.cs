using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

/// <summary>
/// Atributo personalizado para la autorización basada en el tipo de aplicación.
/// </summary>
public class AppAuthorizeAttribute : AuthorizeAttribute
{
    public string AppType { get; set; }

    protected override bool IsAuthorized(HttpActionContext actionContext)
    {
        var headers = actionContext.Request.Headers;
        if (headers.Contains("Authorization"))
        {
            var token = headers.Authorization.Parameter;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(AppType == "desktop" ? "YourDesktossssssssssssssssssssssspSecretKey" : "YourMobissssssssssssssssssssssssssleSecretKey");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = AppType == "desktop" ? "http://panelparent.somee.com/desktop" : "http://panelparent.somee.com/mobile",
                    ValidAudience = AppType == "desktop" ? "http://panelparent.somee.com/desktop" : "http://panelparent.somee.com/mobile",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        return false;
    }

    protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
    {
        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Token inválido o faltante.");
    }
}
