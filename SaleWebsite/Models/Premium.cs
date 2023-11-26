namespace SaleWebsite.Models
{
    public class Premium
    {
        public int PremiumId { get; set; }
        public string Option { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate => StartDate.AddDays(int.Parse(Option));
        public int ProductId { get; set; }
        public Product? Product { get; set; }

    }
}
