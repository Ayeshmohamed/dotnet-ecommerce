using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apps.Entities
{
    public class Product
    {
        public int Id { get; set;}
        [Required]
        public string Name { get; set;} = string.Empty;
        [Required]
        [Column(TypeName ="decimal(6, 2)")]
        public decimal Price { get; set;}
        public string Description { get; set;} = string.Empty;
        public int CategoryId { get; set;}

        public Category Category { get; set; } = null!;

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



    }
}
