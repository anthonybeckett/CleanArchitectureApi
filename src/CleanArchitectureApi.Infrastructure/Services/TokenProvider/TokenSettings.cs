namespace CleanArchitectureApi.Infrastructure.Services.TokenProvider;

public class TokenSettings
{
    public string Audience { get; set; }

    public string Issuer { get; set; }

    public string Secret { get; set; }

    public int TokenValidityInMinutes { get; set; }

    public int RefreshTokenValidityInDates { get; set; }
}