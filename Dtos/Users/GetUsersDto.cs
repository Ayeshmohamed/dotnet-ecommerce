using Apps.Entities;
using System.ComponentModel.DataAnnotations;

namespace Apps.Dtos.Users
{
    public class GetUsersDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public int Status { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
