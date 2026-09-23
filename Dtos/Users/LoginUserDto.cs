using System.ComponentModel.DataAnnotations;

namespace Apps.Dtos.Users
{
    public record LoginUserDto
    (
        [Required][EmailAddress]
        string Email,
        [Required]
        string Password
    );
}
