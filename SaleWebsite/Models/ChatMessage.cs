using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleWebsite.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string ViewStatus { get; set; } = string.Empty.ToString();
        public string Message { get; set; } = string.Empty.ToString();
        public DateTime Timestamp { get; set; }

        [ForeignKey(nameof(Chat))]
        public int ChatId { get; set; }

        [ForeignKey(nameof(Sender))]
        public int SenderId { get; set; }

        [ForeignKey(nameof (Receiver))]
        public int ReceiverId { get; set; }

        public User? Sender { get; set; }
        public User? Receiver { get; set; }

        public Chat? Chat { get; set; }
    }
}
