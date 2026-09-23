using System.ComponentModel.DataAnnotations;

namespace Apps.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; }

        public ICollection<Product> products { get; set; } = null!;
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
