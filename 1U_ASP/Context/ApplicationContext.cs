using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using _1U_ASP.Const;
using _1U_ASP.Models;
using _1U_ASP.Security.Model;
using Dap1U.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace _1U_ASP.Context
{
    public class ApplicationContext : IdentityDbContext<AppUser, AppRole, string, AppUserClaim,
        AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        
        public DbSet<Product> Products { get; set; }
        public DbSet<DocEnterProduct> DocEnterProducts { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<DocEnterProductDetail> DocEnterProductDetails { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleOrderDetail> SaleOrderDetails { get; set; }
        public DbSet<SalePriseDoc> SalePriseDocs { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<ShopProduct> ShopProduct { get; set; }
        public DbSet<ShopBalanceGood> ShopBalanceGood { get; set; }
        public virtual DbSet<SysCode> SysCode { get; set; }

        public virtual DbSet<AspNetUserClaimRole> AspNetUserClaimRole { get; set; }
        public virtual DbSet<AppPerson> ApplicationPerson { get; set; }
        private List<Product> _seedProducts;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            _seedProducts = new List<Product>();

            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
            // optionsBuilder.UseSqlServer(@"Data Source=Server=tcp:serv20.database.windows.net,1433;Initial Catalog=DB1;Persist Security Info=False;User ID=ura19923;Password=sANTON000;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //   optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-TH6PGTN;Initial Catalog=1UT2;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //   optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

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

               
                _seedProducts.Add( new Product { ProductId = 1, Barcode = 4802221111, Name = "Coca-Cola", Description = "500ml", PriceCost = 4, PriseSale = 5 });
                _seedProducts.Add(new Product { ProductId = 2, Barcode = 4802221222, Name = "Sprite", Description = "500ml", PriceCost = 3, PriseSale = 4 });
                _seedProducts.Add(new Product { ProductId = 3, Barcode = 4802221333, Name = "Fanta", Description = "500ml", PriceCost = 4, PriseSale = 4 });
                _seedProducts.Add(new Product { ProductId = 4, Barcode = 4802221334, Name = "Schweppes", Description = "500ml", PriceCost = 4, PriseSale = 5 });
                _seedProducts.Add(new Product { ProductId = 5, Barcode = 4802221335, Name = "Pepsi", Description = "333ml", PriceCost = 3.5, PriseSale = 4.5 });
                _seedProducts.Add(new Product { ProductId = 6, Barcode = 4802221335, Name = "Mirinda", Description = "333ml", PriceCost = 3, PriseSale = 4 });

                _seedProducts.ForEach(x=> entity.HasData(x) );
                
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasKey(e => e.ShopId);

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);

                entity.HasMany(c => c.SaleOrders)
                    .WithOne(e => e.Shop)
                    .HasForeignKey(c => c.ShopId); 

                entity.HasData(
                    new Shop { ShopId = 1, Address = "Virmenska street 100", Name = "Eco-Shop" },
                    new Shop { ShopId = 2, Address = "Ukrainska street 31 ", Name = "Food-world" },
                    new Shop { ShopId = 3, Address = "Ukrainska street 31 ", Name = "House-shop" }
                );
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

                entity.HasData(
                    new ShopProduct { ShopProductId = 1, ShopId = 1, ProductId = 1 },
                    new ShopProduct { ShopProductId = 2, ShopId = 1, ProductId = 2 },
                    new ShopProduct { ShopProductId = 3, ShopId = 1, ProductId = 3 },
                    new ShopProduct { ShopProductId = 4, ShopId = 2, ProductId = 1 },
                    new ShopProduct { ShopProductId = 5, ShopId = 2, ProductId = 2 },
                    new ShopProduct { ShopProductId = 6, ShopId = 2, ProductId = 3 }
                );
            });
            
            modelBuilder.Entity<SaleOrder>(entity =>
            {
                entity.HasKey(e => e.SaleOrderId);

              //  entity.Property(e => e.SaleOrderId).ValueGeneratedOnAdd();

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);

                entity.HasMany(c => c.SaleOrderDetails)
                    .WithOne(e => e.SaleOrder)
                    .HasForeignKey(c => c.SaleOrderDetailId);
                List<SaleOrder> seedSaleOrders = new List<SaleOrder>();
                
                for (int i = 1; i < NumericConstants.ConutSeedSaleOrder; i++)
                {
                    Random rnd = new Random();
                    var randomTest = new Random();
                    TimeSpan timeSpan = DateTime.Now - DateTime.Today.AddYears(-1); ;
                    TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
                    DateTime newDate = DateTime.Today.AddYears(-1) + newSpan;
                   
                    seedSaleOrders.Add(new SaleOrder { SaleOrderId = i, DataTime = newDate, ShopId = rnd.Next(1, NumericConstants.SeedShopMax) });
                }
                seedSaleOrders.ForEach(x=> entity.HasData(x));

                //entity.HasData(
                //    new SaleOrder { SaleOrderId = 1, DataTime = DateTime.Now, ShopId = 1 },
                //    new SaleOrder { SaleOrderId = 2, DataTime = DateTime.Now, ShopId = 2 }

                //);
            });

            modelBuilder.Entity<SaleOrderDetail>(entity =>
            {
                entity.HasKey(e => e.SaleOrderDetailId);

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);

                entity.HasOne(c => c.SaleOrder)
                    .WithMany(e => e.SaleOrderDetails)
                    .HasForeignKey(c => c.SaleOrderId);

                List<SaleOrderDetail> n = new List<SaleOrderDetail>();
                int currentSaleOrderDetailId = 0;
                for (int i = 1; i < NumericConstants.ConutSeedSaleOrder; i++)
                {
                    Random rnd = new Random();
                    int c = rnd.Next(1, 10);
                    for (int y = 1; y < c; y++)
                    {
                        int pr = rnd.Next(1, 6);
                        Product currentProduct = _seedProducts.FirstOrDefault(x => x.ProductId == pr);
                        int countSale = rnd.Next(1, 10);
                        if (currentProduct != null)
                            n.Add(new SaleOrderDetail
                            {
                                SaleOrderDetailId = ++currentSaleOrderDetailId, SaleOrderId = i, Count = countSale,
                                ProductId = currentProduct.ProductId, PriceCost = currentProduct.PriceCost,
                                PriseSale = currentProduct.PriseSale, Summ = countSale * currentProduct.PriseSale
                            });
                    }
                }

                n.ForEach(x => entity.HasData(x));

                //entity.HasData(
                //    new SaleOrderDetail { SaleOrderDetailId = 1, SaleOrderId = 1, Count = 3, ProductId = 1, PriceCost = 12.5, PriseSale = 15, Summ = 3*15},
                //    new SaleOrderDetail { SaleOrderDetailId = 2, SaleOrderId = 1, Count = 2, ProductId = 2, PriceCost = 14.3, PriseSale = 17, Summ = 2*17 },
                //    new SaleOrderDetail { SaleOrderDetailId = 3, SaleOrderId = 2, Count = 5, ProductId = 1, PriceCost = 12.5, PriseSale = 15, Summ = 5*15},
                //    new SaleOrderDetail { SaleOrderDetailId = 4, SaleOrderId = 2, Count = 4, ProductId = 2, PriceCost = 14.3, PriseSale = 17, Summ = 4*17},
                //    new SaleOrderDetail { SaleOrderDetailId = 5, SaleOrderId = 2, Count = 1, ProductId = 3, PriceCost = 16, PriseSale = 19.5, Summ = 1*19.5}
                //    );
            });
            
            modelBuilder.Entity<Provider>(entity =>
            {
                entity.HasKey(e => e.ProviderId);

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);

                entity.HasMany(c => c.DocEnterProducts)
                    .WithOne(e => e.Provider)
                    .HasForeignKey(c => c.ProviderId);

                entity.HasData(
                    new Provider {ProviderId = 1,Name = "The Coca-Cola Company", Address = "Shevchenko stree 34", Phone = 5241234123},
                    new Provider {ProviderId = 2,Name = "PepsiCo", Address = "Hrushevskoho Street 112", Phone = 32121234123}
                );
                //
            });

            modelBuilder.Entity<DocEnterProduct>(entity =>
            {
                entity.HasKey(e => e.DocEnterProductId);

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);

                entity.HasMany(c => c.DocEnterProductDetails)
                    .WithOne(e => e.DocEnterProduct)
                    .HasForeignKey(c => c.DocEnterProductDetailId);

                entity.HasData(
                    new DocEnterProduct { DocEnterProductId =1, ProviderId = 1 },
                    new DocEnterProduct { DocEnterProductId = 2, ProviderId = 2 }
                );

            });

            modelBuilder.Entity<DocEnterProductDetail>(entity =>
            {
                entity.HasKey(e => e.DocEnterProductDetailId);

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);

                entity.HasOne(c => c.DocEnterProduct)
                    .WithMany(e => e.DocEnterProductDetails)
                    .HasForeignKey(c => c.DocEnterProductDetailId);

                entity.HasData(
                    new DocEnterProductDetail { DocEnterProductDetailId =1, DocEnterProductId = 1, ProductId = 1, Count= 7, InPrise  = 5, Summ =  35},
                    new DocEnterProductDetail { DocEnterProductDetailId = 2, DocEnterProductId = 2, ProductId = 2, Count = 6, InPrise = 6, Summ = 36 }
                );

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




            modelBuilder.Entity<AppUser>(b =>
            {
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasOne(d => d.Person)
                   .WithMany(p => p.AspNetUsers)
                   .HasForeignKey(d => d.PersonId)
                   .HasConstraintName("FK_AspNetUsers_Person");
            });

            modelBuilder.Entity<AppRole>(b =>
            {
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();

                b.HasData(
                            new AppRole { Id = "0f26382a-ec94-401e-aef8-e2c44b376858", ConcurrencyStamp = "e6b70a9b-1e2f-48d6-aecc-a31748bdd35f", Name = "companyadmin", NormalizedName = "COMPANYADMIN" },
                            new AppRole { Id = "0f4e8d62-a445-4626-b2c2-d1fc566a7184", ConcurrencyStamp = "223bf724-59cb-405c-aebe-f7ba30d61eb0", Name = "employee", NormalizedName = "EMPLOYEE" },
                           // new AppRole { Id = "13c4dd37-fe45-4d87-8926-5662547cf1b2", ConcurrencyStamp = "edf487c0-47c6-4cbf-8fae-316a52b7c7e8", Name = "jobseeker", NormalizedName = "JOBSEEKER" },
                           // new AppRole { Id = "17d7983c-96e8-4d54-b51c-7692feb9a595", ConcurrencyStamp = "a051d63a-e8a3-4bdf-b60e-2c337f023d19", Name = "customeradminmanager", NormalizedName = "CUSTOMERADMINMANAGER" },
                            new AppRole { Id = "5bb27bb1-913a-4516-92b6-fd8f667275aa", ConcurrencyStamp = "8e193d2e-2a2a-45b1-ae7e-92c28af243ff", Name = "companymanager", NormalizedName = "COMPANYMANAGER" },
                            new AppRole { Id = "9367245f-714a-4843-a58d-276e8200ddf4", ConcurrencyStamp = "20a5aed0-6fc7-49b5-96ea-2a62c3cb17b6", Name = "companyowner", NormalizedName = "COMPANYOWNER" }
                          //  new AppRole { Id = "9cb615ae-3ddc-40da-8d03-65216fe22377", ConcurrencyStamp = "3f827b0d-75fa-4a2e-99c1-540603ca5b54", Name = "superadmin", NormalizedName = "SUPERADMIN" }
                    );

            });

            modelBuilder.Entity<AppUserClaim>(b =>
            {
                b.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                b.Property(e => e.ClaimProperty)
                    .HasMaxLength(100);

                b.HasOne(d => d.User)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserClaimRole>(entity =>
            {
                entity.Property(e => e.AspNetUserClaimRoleId)
                .HasColumnName("AspNetUserClaimRoleID");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.UserClaim)
                    .WithMany(p => p.UserClaimRoles)
                    .HasForeignKey(d => d.ClaimId)
                    .HasConstraintName("FK_AspNetUserClaimRole_AspNetUserClaims_UserId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserClaimRoles)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AppPerson>(entity =>
            {
                entity.ToTable("Person", "Core");

                entity.Property(e => e.PersonId)
                    .HasColumnName("PersonID");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(100);

                entity.HasIndex(e => e.EmailAddress)
                    .HasName("UQ__Person__49A147407F7850CA")
                    .IsUnique();

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(90);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.GenderSysCodeUniqueId)
                    .HasColumnName("GenderSysCodeUniqueID");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(20);

                entity.Property(e => e.UserActionId)
                    .HasColumnName("UserActionID");
            });

            modelBuilder.Entity<SysCode>(entity =>
            {
                entity.HasIndex(e => e.UniqueId)
                    .HasName("IX_UniqueID")
                    .IsUnique();

                entity.HasIndex(e => new { e.Deleted, e.ShortDesc })
                    .HasName("nci_wi_SysCode_A450FA7C09A5ABDEA56D664EF1C878CA");

                entity.HasIndex(e => new { e.Deleted, e.SysCode1, e.CodeId })
                    .HasName("nci_wi_SysCode_2F628BBB1C7C43B1E8662FE58A7B34AC");

                entity.Property(e => e.SysCodeId).HasColumnName("SysCodeID");

                entity.Property(e => e.AddDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AddUser)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.ChangeDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ChangeUser)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.CodeId)
                    .IsRequired()
                    .HasColumnName("CodeID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.Group1)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LongDesc).IsUnicode(false);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ShortDesc)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SysCode1)
                    .IsRequired()
                    .HasColumnName("SysCode")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UniqueId).HasColumnName("UniqueID");
            });
        }
         
        public DbSet<Person> Person { get; set; }






    }
}
