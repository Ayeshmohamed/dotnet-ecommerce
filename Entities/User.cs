using System.ComponentModel.DataAnnotations;

namespace Apps.Entities
{
    public class User
    {
        public int Id {get; set;}
        [Required]
        public string Name {get; set;}

        public string Email {get; set;}

        public int Status {get; set;}

        public string Phone {get; set;}

        public string Address {get; set;}

        public string Password {get; set;}

        public bool IsActive {get; set;}

        public DateTime CreatedAt {get; set;}

        public DateTime UpdatedAt {get; set;}

        public ICollection<Order> Orders {get; set;}

    }
}
