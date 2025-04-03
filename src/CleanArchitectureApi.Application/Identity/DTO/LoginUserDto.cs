using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureApi.Application.Identity.DTO;

public class LoginUserDto
{
    [Required]
    [MaxLength(256)]
    [EmailAddress]
    public string Email { get; set; }

    [Required] public string Password { get; set; }
}