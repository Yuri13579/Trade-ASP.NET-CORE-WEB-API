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

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-TH6PGTN;Initial Catalog=1UT;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
          // optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasMany(c => c.SaleOrderDetails)
                    .WithOne(e => e.Product);

                entity.HasMany(c => c.DocEnterProducts)
                    .WithOne(e => e.Product);

                entity.HasMany(c => c.SalePriseDocs)
                    .WithOne(e => e.Product);

                //entity.HasOne(d => d.Product)
                //    .WithMany(p => p.ProductId)
                //    .HasForeignKey(d => d.ProductId);
            });






        }






    }
}
