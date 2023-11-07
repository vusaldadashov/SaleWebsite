namespace SaleWebsite.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        // Define other user properties, e.g., Name, Address, etc.

        public bool IsAdmin { get; set; }
        public ICollection<Product>? Products { get; set; }
        public Membership? Membership { get; set; }

        public ICollection<ChatMessage>? ChatMessages { get; set; }
    }

}
