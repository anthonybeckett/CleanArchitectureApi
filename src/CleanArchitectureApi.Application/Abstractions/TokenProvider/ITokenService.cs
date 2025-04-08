using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CleanArchitectureApi.Domain.Identity.Users.Entities;

namespace CleanArchitectureApi.Application.Abstractions.TokenProvider;

public interface ITokenService
{
    Task<JwtSecurityToken> CreateTokenAsync(AppUser user, IList<string> roles);

    string GenerateRefreshToken();
    
    ClaimsPrincipal GetPrincipalFromExpiredToken(string? token);
    
    DateTime GetRefreshTokenExpirationDate();
}