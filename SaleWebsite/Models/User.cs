namespace SaleWebsite.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Surname { get; set; } = string.Empty;

    public string Img { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    public string Phone { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;

    public int IsAdmin { get; set; }

    public ICollection<Vip>? Vips { get; set; }
    public ICollection<Product>? Products { get; set; }

    public ICollection<ChatMessage>? SendMessages { get; set; }

    public ICollection<ChatMessage>? ReceiveMessages { get; set; }

    public ICollection<Chat>? Chats { get; set; }
}
