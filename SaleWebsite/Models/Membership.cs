namespace SaleWebsite.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Membership
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public decimal Price { get; set; }

        // Define other membership properties.

        
    }

}
