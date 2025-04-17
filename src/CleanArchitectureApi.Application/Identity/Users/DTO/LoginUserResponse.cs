using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Application.Identity.Users.DTO;

public class LoginUserResponse : IResult
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpireDate { get; set; }
}