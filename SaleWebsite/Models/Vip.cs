namespace SaleWebsite.Models
{
    public class Vip
    {
        public int VipId { get; set; }

        public string Option { get; set; } = "0";

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate => StartDate.AddDays(int.Parse(Option));

        public int UserId { get; set; }

        public User? User { get; set; }
    }
}
