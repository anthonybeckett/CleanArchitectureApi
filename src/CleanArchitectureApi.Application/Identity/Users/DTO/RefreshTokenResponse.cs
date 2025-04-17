using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Application.Identity.Users.DTO;

public class RefreshTokenResponse : IResult
{
    public string NewAccessToken { get; set; }
    
    public string NewRefreshToken { get; set; }
    
    public DateTime RefreshTokenExpireDate { get; set; }
}