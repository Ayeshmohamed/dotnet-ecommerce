using Apps.Dtos.Categories;
using Apps.Entities;

namespace Apps.Dtos.Products
{
    public class GetProductsDto
    {
       public int Id { get; set; }
       public string Name { get; set; }
       public decimal Price { get; set; }
       public string Description {  get; set; }
        public GetCategoryDto Category { get; set; } = null!;

        public string Slug { get; set; } = string.Empty!;
        public string Sku { get; set; } = string.Empty!;

        public decimal? DiscountPrice { get; set; }

        public bool IsActive { get; set; } = false;

        public bool IsFeatured { get; set; } = false;

        public int Quantity { get; set; }

        public decimal Weight { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; } 
    };
}
