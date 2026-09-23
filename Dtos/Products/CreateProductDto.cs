using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Apps.Dtos.Products
{

    public record CreateProductDto
    (

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
         string Name,

        [Required]
        [Range(1, 100000)]
         decimal Price,

        [DataType("string")]
        [StringLength(999)]
         string? Description,

         [Required]
         int CategoryId,

         string Sku,
         [Range(1, 10000)]
         decimal? DiscountPrice,
          
          bool IsActive ,

          bool IsFeatured ,
          [Required][Range(1,10000)]
          int Quantity ,

          [Range(1, 10000)]
          decimal Weight ,

         List<CreateProductImageDto> Images

    );
}
