using System.Text;
using Microsoft.IdentityModel.Tokens;
using Owin;
using System.Web.Http;
using API_PANEL_PARENTS;
using Microsoft.Owin.Security.Jwt;

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        HttpConfiguration config = new HttpConfiguration();

        ConfigureOAuth(app);

        WebApiConfig.Register(config);
        app.UseWebApi(config);
    }

    public void ConfigureOAuth(IAppBuilder app)
    {
        var desktopKey = Encoding.UTF8.GetBytes("YourDesktossssssssssssssssssssssspSecretKey");
        var mobileKey = Encoding.UTF8.GetBytes("YourMobissssssssssssssssssssssssssleSecretKey");

        // Configuración de autenticación para la aplicación de escritorio
        app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
        {
            AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
            TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "http://panelparent.somee.com/desktop",
                ValidAudience = "http://panelparent.somee.com/desktop",
                IssuerSigningKey = new SymmetricSecurityKey(desktopKey)
            }
        });

        // Configuración de autenticación para la aplicación móvil
        app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
        {
            AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
            TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "http://panelparent.somee.com/mobile",
                ValidAudience = "http://panelparent.somee.com/mobile",
                IssuerSigningKey = new SymmetricSecurityKey(mobileKey)
            }
        });
    }
}
