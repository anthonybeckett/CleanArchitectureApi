using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureApi.Application.Identity.DTO;

public class RevokeUserDto
{
    [Required]
    [MaxLength(256)]
    [EmailAddress]
    public string Email { get; set; }
}