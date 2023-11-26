using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using SaleWebsite.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.Mail;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace SaleWebsite
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Vip> Vips { get; set; }

        public DbSet<Premium> Premiums { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Image>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().HasKey(p => p.UserId);
            modelBuilder.Entity<Premium>().HasKey(p => p.PremiumId);
            modelBuilder.Entity<Vip>().HasKey(v => v.VipId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(c => c.Participants)
                .UsingEntity(j => j.ToTable("UserChat"));

            modelBuilder.Entity<ChatMessage>()
               .HasOne(m => m.Sender)
               .WithMany(u => u.SendMessages)
               .HasForeignKey(m => m.SenderId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceiveMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.ChatMessages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
