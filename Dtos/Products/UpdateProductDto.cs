using System.ComponentModel.DataAnnotations;

namespace Apps.Dtos.Products
{
    public record UpdateProductDto
    (
        [Required]
         int Id,
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

          Boolean IsActive,

          Boolean IsFeatured,
          [Required][Range(1,10000)]
          int Quantity,

          [Range(1, 10000)]
          decimal Weight,

         List<CreateProductImageDto> Images


    );
}
