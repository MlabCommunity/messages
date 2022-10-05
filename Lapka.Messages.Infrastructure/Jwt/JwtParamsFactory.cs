using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Lapka.Messages.Infrastructure.Jwt;

public static class JwtParamsFactory
{
    public static TokenValidationParameters CreateParameters(JwtSettings settings)
    {
        var publicRsa = RSA.Create();
        var publicKey = File.ReadAllText(settings.RsaPublicKeyPath);
        publicRsa.ImportFromPem(publicKey);
        var issuerSigningKey = new RsaSecurityKey(publicRsa);

        return new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidIssuer = settings.Issuer,
            IssuerSigningKey = issuerSigningKey,
            ClockSkew = settings.ClockSkew
        };
    }
}