using System.ComponentModel.DataAnnotations.Schema;

namespace SaleWebsite.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public User? User { get; set; }
    }
}
