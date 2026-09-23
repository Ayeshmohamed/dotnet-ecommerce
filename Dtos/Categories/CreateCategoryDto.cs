using System.ComponentModel.DataAnnotations;

namespace Apps.Dtos.Categories
{
    public record CreateCategoryDto
    (
        [Required][MinLength(3)][MaxLength(50)]
         string Name,
        [MaxLength(255)]
         string Description,
         IFormFile? Image,
         int ?ParentId,
         bool IsActive
    );
}
