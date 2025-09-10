namespace CleanArchitectureApi.Domain.Identity.Jwt.ValueObjects;

public class JwtSettings
{
    public string? Audience { get; init; }
    public string? Issuer { get; init; }
    public string Secret { get; init; } = null!;
    public int TokenValidityInMinutes { get; init; }
    public int RefreshTokenValidityInDays { get; init; }
}