using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleWebsite.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

        // Many-to-Many relationship with Participants
        public ICollection<User> Participants { get; set; } = new List<User>();
    }
}
