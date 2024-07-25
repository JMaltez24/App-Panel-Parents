using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Clase estática para la generación de tokens JWT.
/// </summary>
public static class JwtTokenGenerator
{
    // Clave secreta para la firma del token de la aplicación de escritorio.
    private const string DesktopSecretKey = "YourDesktossssssssssssssssssssssspSecretKey";

    // Clave secreta para la firma del token de la aplicación móvil.
    private const string MobileSecretKey = "YourMobissssssssssssssssssssssssssleSecretKey";

    /// <summary>
    /// Genera un token JWT para un usuario especificado y un tipo de aplicación.
    /// </summary>
    /// <param name="username">Nombre de usuario.</param>
    /// <param name="appType">Tipo de aplicación ("desktop" o "mobile").</param>
    /// <returns>Token JWT generado.</returns>
    public static string GenerateToken(string username, string appType)
    {
        // Crear la clave de seguridad usando la clave secreta correspondiente al tipo de aplicación.
        var key = Encoding.UTF8.GetBytes(appType == "desktop" ? DesktopSecretKey : MobileSecretKey);
        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Crear los claims (reclamaciones) que contendrá el token.
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Crear el token con la información especificada.
        var token = new JwtSecurityToken(
            issuer: appType == "desktop" ? "http://panelparent.somee.com/desktop" : "http://panelparent.somee.com/mobile",
            audience: appType == "desktop" ? "http://panelparent.somee.com/desktop" : "http://panelparent.somee.com/mobile",
            claims: claims,
            expires: DateTime.Now.AddMinutes(300),
            signingCredentials: credentials);

        // Devolver el token como una cadena.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
