using System.ComponentModel.DataAnnotations;

namespace Apps.Dtos.Users
{
    public record CreateUserDto
    (
        [Required]
         string Name ,
         [Required][EmailAddress]
         string Email ,
         [Required][Phone]
         string Phone ,
         [Required][StringLength(255)][MinLength(5)]
         string Address ,
         bool IsActive ,
         [Required]
         string Password

     );
    
}
