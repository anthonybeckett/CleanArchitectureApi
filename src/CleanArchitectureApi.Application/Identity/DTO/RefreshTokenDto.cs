using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureApi.Application.Identity.DTO;

public class RefreshTokenDto
{
    [Required] public string AccessToken { get; set; }

    [Required] public string RefreshToken { get; set; }
}