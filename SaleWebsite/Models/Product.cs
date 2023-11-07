namespace SaleWebsite.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;
        [Required]
        public string? Description { get; set; }

        [Required]
        public int Price { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();

        [Required]
        public string? Categories { get; set; }

        [Required]
        public string? Condition { get; set; }

        // Define other product properties, e.g., Category, Condition, etc.

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
