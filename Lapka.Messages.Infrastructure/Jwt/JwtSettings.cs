namespace Lapka.Messages.Infrastructure.Jwt;

public class JwtSettings
{
    public string Issuer { get; set; }
    public string RsaPublicKeyPath { get; set; }
    public TimeSpan ClockSkew { get; set; }
}