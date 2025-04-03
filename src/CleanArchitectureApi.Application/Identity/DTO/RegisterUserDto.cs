using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureApi.Application.Identity.DTO;

public class RegisterUserDto
{
    [Required] [MaxLength(50)] public string Fullname { get; set; }

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; }

    [Required] public string Password { get; set; }

    [Required] [Compare("Password")] public string ConfirmPassword { get; set; }

    public string? AdminKey { get; set; }
}