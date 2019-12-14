using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dap1U.Models;
using _1U_ASP.Models;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _1U_ASP.Context
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<DocEnterProduct> DocEnterProducts { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<DocEnterProductDetail> DocEnterProductDetails { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleOrderDetail> SaleOrderDetails { get; set; }
        public DbSet<SalePriseDoc> SalePriseDocs { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<ShopProduct> ShopProduct { get; set; }
        public DbSet<ShopBalanceGood> ShopBalanceGood { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-TH6PGTN;Initial Catalog=1UT4;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
          // optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);
                
                entity.HasMany(c => c.SaleOrderDetails)
                    .WithOne(e => e.Product)
                    .HasForeignKey(c => c.ProductId);
                
                entity.HasMany(c => c.SalePriseDocs)
                    .WithOne(e => e.Product)
                    .HasForeignKey(c => c.ProductId);

                entity.HasData(
                    new Product { ProductId = 1, Barcode = 4802221111, Name = "Fairy", Description = "500ml"},
                    new Product { ProductId = 2, Barcode = 4802221222, Name = "Fairy", Description = "250ml" },
                    new Product { ProductId = 3, Barcode = 4802221333, Name = "Gala", Description = "500ml" }
                );
            });

            modelBuilder.Entity<SaleOrder>(entity =>
            {
                entity.HasKey(e => e.SaleOrderID);
                
                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);
                
                entity.HasMany(c => c.SaleOrderDetails)
                    .WithOne(e => e.SaleOrder)
                    .HasForeignKey(c => c.SaleOrderDetailId);

            });


            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasKey(e => e.ShopId);

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);

                entity.HasMany(c => c.SaleOrders)
                    .WithOne(e => e.Shop)
                    .HasForeignKey(c => c.SaleOrderID); ;
            });

            modelBuilder.Entity<ShopProduct>(entity =>
            {
                entity.HasKey(e => e.ShopProductId);

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);

                entity.HasOne(c => c.Shop)
                    .WithMany(e => e.ShopProducts)
                    .HasForeignKey(c => c.ShopId); ;

                entity.HasOne(c => c.Product)
                    .WithMany(e => e.ShopProducts)
                    .HasForeignKey(c => c.ProductId); ;
            });
            
            modelBuilder.Entity<DocEnterProduct>(entity =>
            {
                entity.HasKey(e => e.DocEnterProductId);

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);

                entity.HasMany(c => c.DocEnterProductDetails)
                    .WithOne(e => e.DocEnterProduct)
                   .HasForeignKey(c => c.DocEnterProductDetailId);
                

            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.HasKey(e => e.ProviderId);

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);

                entity.HasMany(c => c.DocEnterProducts)
                    .WithOne(e => e.Provider)
                    .HasForeignKey(c => c.ProviderId);
                
            });

            modelBuilder.Entity<ShopBalanceGood>(entity =>
            {
                entity.HasKey(e => e.ShopBalanceGoodId);

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);

                entity.HasOne(c => c.DocEnterProduct)
                    .WithMany(e => e.ShopBalanceGood)
                    .HasForeignKey(c => c.DocEnterProductId);

                entity.HasOne(c => c.Product)
                    .WithMany(e => e.ShopBalanceGood)
                    .HasForeignKey(c => c.ProductId);

                entity.HasOne(c => c.Shop)
                    .WithMany(e => e.ShopBalanceGood)
                    .HasForeignKey(c => c.ShopId);

            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.HasKey(e => e.ProviderId);

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);

                entity.HasMany(c => c.DocEnterProducts)
                    .WithOne(e => e.Provider)
                    .HasForeignKey(c => c.DocEnterProductId);

           });



        }






    }
}
