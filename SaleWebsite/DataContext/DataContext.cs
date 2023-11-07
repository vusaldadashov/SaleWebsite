using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using SaleWebsite.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.Mail;
using System.Net.Sockets;

namespace SaleWebsite
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Image>().HasKey(p => p.Id);
            modelBuilder.Entity<Membership>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
        }
    }
}
