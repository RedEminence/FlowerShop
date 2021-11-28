using FlowerShop.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
      
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Flower> Flowers { get; set; }

        public DbSet<Bouquet> Bouquets { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
             builder.Entity<Bouquet>()
                .HasMany(b => b.Flowers)
                .WithMany(b => b.Bouquets)
                .UsingEntity<FlowerBouquet>(
                    j => j
                        .HasOne(pt => pt.Flower)
                        .WithMany(f => f.FlowerBouquets)
                        .HasForeignKey(pt => pt.FlowerId),
                    j => j
                        .HasOne(pt => pt.Bouquet)
                        .WithMany(b => b.FlowerBouquets)
                        .HasForeignKey(pt => pt.BouquetId),
                    j =>
                    {
                        j.Property(pt => pt.FlowerCount).HasDefaultValue(1);
                        j.HasKey(f => new { f.FlowerId, f.BouquetId });
                    });

            builder.Entity<IdentityRole>().HasData(
                new { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Client", NormalizedName = "CLIENT" },
                new { Id = "2c5e174e-3b0e-446f-86af-483d56fd7320", Name = "Manager", NormalizedName = "MANAGER" }
            );

            builder.Entity<User>().HasData(
                new
                {
                    Id = "e1746ac3-bc36-4296-9993-644ec1894298",
                    UserName = "Пользователь-менеджер",

                    // manager
                    PasswordHash = "AQAAAAEAACcQAAAAEB/56cHKJe3B55eGS70Un+KBcOiyMGxrNhLo6yMfLb3KDEwAVgQh9LQQ6+/sulZkTw==",

                    Email = "manager@example.com",
                    EmailConfirmed = true,
                    AccessFailedCount = 0,
                    LockoutEnabled = true,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    SecurityStamp = "6275F5YJHD32NVDF7R3BD2MEENA3V5GO",
                    NormalizedUserName = "MANAGER@EXAMPLE.COM",
                    NormalizedEmail = "MANAGER@EXAMPLE.COM"
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7320",
                    UserId = "e1746ac3-bc36-4296-9993-644ec1894298"
                }
            );

            builder.Entity<Flower>().HasData(
                new Flower { Id = 1, Name = "Роза" },
                new Flower { Id = 2, Name = "Хризантема" },
                new Flower { Id = 3, Name = "Орхидея" }
            );

            builder.Entity<Bouquet>().HasData(
                new Bouquet { Id = 1, Name = "Вдохновение", Description = "Описание букета Вдохновение", Price = 100, Image = "vdohnovenie.jpg" },
                new Bouquet { Id = 2, Name = "Нежность", Description = "Описание букета Нежность", Price = 200, Image = "ocharovanie.jpg" },
                new Bouquet { Id = 3, Name = "Очарование", Description = "Описание букета Очарование", Price = 300, Image = "nezhnost.jpg" }
            );

            builder.Entity<FlowerBouquet>().HasData(
                    new { BouquetId = 1, FlowerId = 1, FlowerCount = 3 },

                    new { BouquetId = 2, FlowerId = 1, FlowerCount = 1 },
                    new { BouquetId = 2, FlowerId = 2, FlowerCount = 1 },
                    new { BouquetId = 2, FlowerId = 3, FlowerCount = 1 },

                    new { BouquetId = 3, FlowerId = 2, FlowerCount = 2 },
                    new { BouquetId = 3, FlowerId = 3, FlowerCount = 2 }
            );

            builder.Entity<OrderStatus>().HasData(
                new { Id = 1, Name = "Принят", },
                new { Id = 2, Name = "В работе" },
                new { Id = 3, Name = "Завершен" }
            );

            base.OnModelCreating(builder);
        }
    }
}