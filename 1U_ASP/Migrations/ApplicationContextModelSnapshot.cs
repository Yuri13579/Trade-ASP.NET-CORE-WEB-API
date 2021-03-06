﻿// <auto-generated />
using System;
using _1U_ASP.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace _1U_ASP.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dap1U.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Barcode");

                    b.Property<int>("CategoryId");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<double>("PriceCost");

                    b.Property<double>("PriseSale");

                    b.Property<int?>("UserActionId");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Barcode = 4802221111.0,
                            CategoryId = 0,
                            Deleted = false,
                            Description = "500ml",
                            Name = "Coca-Cola",
                            PriceCost = 4.0,
                            PriseSale = 5.0
                        },
                        new
                        {
                            ProductId = 2,
                            Barcode = 4802221222.0,
                            CategoryId = 0,
                            Deleted = false,
                            Description = "500ml",
                            Name = "Sprite",
                            PriceCost = 3.0,
                            PriseSale = 4.0
                        },
                        new
                        {
                            ProductId = 3,
                            Barcode = 4802221333.0,
                            CategoryId = 0,
                            Deleted = false,
                            Description = "500ml",
                            Name = "Fanta",
                            PriceCost = 4.0,
                            PriseSale = 4.0
                        },
                        new
                        {
                            ProductId = 4,
                            Barcode = 4802221334.0,
                            CategoryId = 0,
                            Deleted = false,
                            Description = "500ml",
                            Name = "Schweppes",
                            PriceCost = 4.0,
                            PriseSale = 5.0
                        },
                        new
                        {
                            ProductId = 5,
                            Barcode = 4802221335.0,
                            CategoryId = 0,
                            Deleted = false,
                            Description = "333ml",
                            Name = "Pepsi",
                            PriceCost = 3.5,
                            PriseSale = 4.5
                        },
                        new
                        {
                            ProductId = 6,
                            Barcode = 4802221335.0,
                            CategoryId = 0,
                            Deleted = false,
                            Description = "333ml",
                            Name = "Mirinda",
                            PriceCost = 3.0,
                            PriseSale = 4.0
                        });
                });

            modelBuilder.Entity("_1U_ASP.Models.DocEnterProduct", b =>
                {
                    b.Property<int>("DocEnterProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int?>("ProductId");

                    b.Property<int?>("ProviderId");

                    b.Property<int?>("UserActionId");

                    b.HasKey("DocEnterProductId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProviderId");

                    b.ToTable("DocEnterProducts");
                });

            modelBuilder.Entity("_1U_ASP.Models.DocEnterProductDetail", b =>
                {
                    b.Property<int>("DocEnterProductDetailId");

                    b.Property<int>("Count");

                    b.Property<bool>("Delete");

                    b.Property<int?>("DocEnterProductId");

                    b.Property<float>("InPrise");

                    b.Property<int?>("ProductId");

                    b.Property<double>("Summ");

                    b.Property<int>("UserActionId");

                    b.HasKey("DocEnterProductDetailId");

                    b.HasIndex("ProductId");

                    b.ToTable("DocEnterProductDetails");
                });

            modelBuilder.Entity("_1U_ASP.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("DisplayName");

                    b.Property<DateTime?>("Dob");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<int?>("GenderSysCodeUniqueId");

                    b.Property<string>("LastName");

                    b.Property<string>("MobilePhone");

                    b.Property<int?>("UserActionId");

                    b.HasKey("PersonId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("_1U_ASP.Models.Profile", b =>
                {
                    b.Property<int>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<int>("PersonId");

                    b.Property<string>("ProfileTitle");

                    b.Property<int>("ProfileTypeSysCodeUniqueId");

                    b.Property<bool?>("Status");

                    b.Property<int?>("UserActionId");

                    b.HasKey("ProfileId");

                    b.HasIndex("PersonId");

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("_1U_ASP.Models.Provider", b =>
                {
                    b.Property<int>("ProviderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name");

                    b.Property<decimal>("Phone");

                    b.Property<int?>("UserActionId");

                    b.HasKey("ProviderId");

                    b.ToTable("Provider");

                    b.HasData(
                        new
                        {
                            ProviderId = 1,
                            Address = "Shevchenko stree 34",
                            Deleted = false,
                            Name = "The Coca-Cola Company",
                            Phone = 5241234123m
                        },
                        new
                        {
                            ProviderId = 2,
                            Address = "Hrushevskoho Street 112",
                            Deleted = false,
                            Name = "PepsiCo",
                            Phone = 32121234123m
                        });
                });

            modelBuilder.Entity("_1U_ASP.Models.SaleOrder", b =>
                {
                    b.Property<int>("SaleOrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataTime");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int?>("ShopId");

                    b.Property<int?>("UserActionId");

                    b.HasKey("SaleOrderId");

                    b.HasIndex("ShopId");

                    b.ToTable("SaleOrders");

                    b.HasData(
                        new
                        {
                            SaleOrderId = 1,
                            DataTime = new DateTime(2020, 5, 20, 14, 19, 44, 831, DateTimeKind.Local).AddTicks(6074),
                            Deleted = false,
                            ShopId = 1
                        },
                        new
                        {
                            SaleOrderId = 2,
                            DataTime = new DateTime(2020, 5, 20, 14, 19, 44, 834, DateTimeKind.Local).AddTicks(1867),
                            Deleted = false,
                            ShopId = 2
                        });
                });

            modelBuilder.Entity("_1U_ASP.Models.SaleOrderDetail", b =>
                {
                    b.Property<int>("SaleOrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<bool>("Deleted");

                    b.Property<double?>("PriceCost");

                    b.Property<double?>("PriseSale");

                    b.Property<int?>("ProductId");

                    b.Property<int?>("SaleOrderId");

                    b.Property<double?>("Summ");

                    b.Property<int?>("UserActionId");

                    b.HasKey("SaleOrderDetailId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SaleOrderId");

                    b.ToTable("SaleOrderDetails");

                    b.HasData(
                        new
                        {
                            SaleOrderDetailId = 1,
                            Count = 3,
                            Deleted = false,
                            PriceCost = 12.5,
                            PriseSale = 15.0,
                            ProductId = 1,
                            SaleOrderId = 1,
                            Summ = 45.0
                        },
                        new
                        {
                            SaleOrderDetailId = 2,
                            Count = 2,
                            Deleted = false,
                            PriceCost = 14.300000000000001,
                            PriseSale = 17.0,
                            ProductId = 2,
                            SaleOrderId = 1,
                            Summ = 34.0
                        },
                        new
                        {
                            SaleOrderDetailId = 3,
                            Count = 5,
                            Deleted = false,
                            PriceCost = 12.5,
                            PriseSale = 15.0,
                            ProductId = 1,
                            SaleOrderId = 2,
                            Summ = 75.0
                        },
                        new
                        {
                            SaleOrderDetailId = 4,
                            Count = 4,
                            Deleted = false,
                            PriceCost = 14.300000000000001,
                            PriseSale = 17.0,
                            ProductId = 2,
                            SaleOrderId = 2,
                            Summ = 68.0
                        },
                        new
                        {
                            SaleOrderDetailId = 5,
                            Count = 1,
                            Deleted = false,
                            PriceCost = 16.0,
                            PriseSale = 19.5,
                            ProductId = 3,
                            SaleOrderId = 2,
                            Summ = 19.5
                        });
                });

            modelBuilder.Entity("_1U_ASP.Models.SalePriseDoc", b =>
                {
                    b.Property<int>("SalePriseDocId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateFrom");

                    b.Property<bool>("Deleted");

                    b.Property<int?>("ProductId");

                    b.Property<decimal>("SalePrise");

                    b.Property<int?>("UserActionId");

                    b.HasKey("SalePriseDocId");

                    b.HasIndex("ProductId");

                    b.ToTable("SalePriseDocs");
                });

            modelBuilder.Entity("_1U_ASP.Models.Shop", b =>
                {
                    b.Property<int>("ShopId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name");

                    b.Property<int?>("UserActionId");

                    b.HasKey("ShopId");

                    b.ToTable("Shops");

                    b.HasData(
                        new
                        {
                            ShopId = 1,
                            Address = "Virmenska street 100",
                            Deleted = false,
                            Name = "EcoShop1"
                        },
                        new
                        {
                            ShopId = 2,
                            Address = "Ukrainska street 31 ",
                            Deleted = false,
                            Name = "EcoShop2"
                        });
                });

            modelBuilder.Entity("_1U_ASP.Models.ShopBalanceGood", b =>
                {
                    b.Property<int>("ShopBalanceGoodId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("DocEnterProductId");

                    b.Property<int>("ProductId");

                    b.Property<int>("ShopId");

                    b.Property<int?>("UserActionId");

                    b.HasKey("ShopBalanceGoodId");

                    b.HasIndex("DocEnterProductId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShopId");

                    b.ToTable("ShopBalanceGood");
                });

            modelBuilder.Entity("_1U_ASP.Models.ShopProduct", b =>
                {
                    b.Property<int>("ShopProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("ProductId");

                    b.Property<int>("ShopId");

                    b.Property<int?>("UserActionId");

                    b.HasKey("ShopProductId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShopId");

                    b.ToTable("ShopProduct");

                    b.HasData(
                        new
                        {
                            ShopProductId = 1,
                            Deleted = false,
                            ProductId = 1,
                            ShopId = 1
                        },
                        new
                        {
                            ShopProductId = 2,
                            Deleted = false,
                            ProductId = 2,
                            ShopId = 1
                        },
                        new
                        {
                            ShopProductId = 3,
                            Deleted = false,
                            ProductId = 3,
                            ShopId = 1
                        },
                        new
                        {
                            ShopProductId = 4,
                            Deleted = false,
                            ProductId = 1,
                            ShopId = 2
                        },
                        new
                        {
                            ShopProductId = 5,
                            Deleted = false,
                            ProductId = 2,
                            ShopId = 2
                        },
                        new
                        {
                            ShopProductId = 6,
                            Deleted = false,
                            ProductId = 3,
                            ShopId = 2
                        });
                });

            modelBuilder.Entity("_1U_ASP.Models.SysCode", b =>
                {
                    b.Property<int>("SysCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SysCodeID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("AddUser")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(suser_sname())")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<DateTime>("ChangeDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("ChangeUser")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(suser_sname())")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("CodeId")
                        .IsRequired()
                        .HasColumnName("CodeID")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("Comments")
                        .IsUnicode(false);

                    b.Property<bool>("Deleted");

                    b.Property<bool?>("Group1")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((1))");

                    b.Property<bool>("Group2");

                    b.Property<bool>("Group3");

                    b.Property<bool>("Group4");

                    b.Property<bool>("Group5");

                    b.Property<bool>("Group6");

                    b.Property<string>("LongDesc")
                        .IsUnicode(false);

                    b.Property<int>("OrderId")
                        .HasColumnName("OrderID");

                    b.Property<string>("ShortDesc")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.Property<string>("SysCode1")
                        .IsRequired()
                        .HasColumnName("SysCode")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<int>("UniqueId")
                        .HasColumnName("UniqueID");

                    b.HasKey("SysCodeId");

                    b.HasIndex("UniqueId")
                        .IsUnique()
                        .HasName("IX_UniqueID");

                    b.HasIndex("Deleted", "ShortDesc")
                        .HasName("nci_wi_SysCode_A450FA7C09A5ABDEA56D664EF1C878CA");

                    b.HasIndex("Deleted", "SysCode1", "CodeId")
                        .HasName("nci_wi_SysCode_2F628BBB1C7C43B1E8662FE58A7B34AC");

                    b.ToTable("SysCode");
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppPerson", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PersonID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("Dob")
                        .HasColumnName("DOB")
                        .HasColumnType("date");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(90);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<int?>("GenderSysCodeUniqueId")
                        .HasColumnName("GenderSysCodeUniqueID");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("MobilePhone")
                        .HasMaxLength(20);

                    b.Property<int?>("UserActionId")
                        .HasColumnName("UserActionID");

                    b.HasKey("PersonId");

                    b.HasIndex("EmailAddress")
                        .IsUnique()
                        .HasName("UQ__Person__49A147407F7850CA");

                    b.ToTable("Person","Core");
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "0f26382a-ec94-401e-aef8-e2c44b376858",
                            ConcurrencyStamp = "e6b70a9b-1e2f-48d6-aecc-a31748bdd35f",
                            Name = "companyadmin",
                            NormalizedName = "COMPANYADMIN"
                        },
                        new
                        {
                            Id = "0f4e8d62-a445-4626-b2c2-d1fc566a7184",
                            ConcurrencyStamp = "223bf724-59cb-405c-aebe-f7ba30d61eb0",
                            Name = "employee",
                            NormalizedName = "EMPLOYEE"
                        },
                        new
                        {
                            Id = "13c4dd37-fe45-4d87-8926-5662547cf1b2",
                            ConcurrencyStamp = "edf487c0-47c6-4cbf-8fae-316a52b7c7e8",
                            Name = "jobseeker",
                            NormalizedName = "JOBSEEKER"
                        },
                        new
                        {
                            Id = "17d7983c-96e8-4d54-b51c-7692feb9a595",
                            ConcurrencyStamp = "a051d63a-e8a3-4bdf-b60e-2c337f023d19",
                            Name = "customeradminmanager",
                            NormalizedName = "CUSTOMERADMINMANAGER"
                        },
                        new
                        {
                            Id = "5bb27bb1-913a-4516-92b6-fd8f667275aa",
                            ConcurrencyStamp = "8e193d2e-2a2a-45b1-ae7e-92c28af243ff",
                            Name = "companymanager",
                            NormalizedName = "COMPANYMANAGER"
                        },
                        new
                        {
                            Id = "9367245f-714a-4843-a58d-276e8200ddf4",
                            ConcurrencyStamp = "20a5aed0-6fc7-49b5-96ea-2a62c3cb17b6",
                            Name = "companyowner",
                            NormalizedName = "COMPANYOWNER"
                        },
                        new
                        {
                            Id = "9cb615ae-3ddc-40da-8d03-65216fe22377",
                            ConcurrencyStamp = "3f827b0d-75fa-4a2e-99c1-540603ca5b54",
                            Name = "superadmin",
                            NormalizedName = "SUPERADMIN"
                        });
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<int?>("PersonId");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PersonId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimProperty")
                        .HasMaxLength(100);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppUserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppUserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppUserToken", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AspNetUserClaimRole", b =>
                {
                    b.Property<int>("AspNetUserClaimRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AspNetUserClaimRoleID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClaimId");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.HasKey("AspNetUserClaimRoleId");

                    b.HasIndex("ClaimId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserClaimRole");
                });

            modelBuilder.Entity("_1U_ASP.Models.DocEnterProduct", b =>
                {
                    b.HasOne("Dap1U.Models.Product")
                        .WithMany("DocEnterProducts")
                        .HasForeignKey("ProductId");

                    b.HasOne("_1U_ASP.Models.Provider", "Provider")
                        .WithMany("DocEnterProducts")
                        .HasForeignKey("ProviderId");
                });

            modelBuilder.Entity("_1U_ASP.Models.DocEnterProductDetail", b =>
                {
                    b.HasOne("_1U_ASP.Models.DocEnterProduct", "DocEnterProduct")
                        .WithMany("DocEnterProductDetails")
                        .HasForeignKey("DocEnterProductDetailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dap1U.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("_1U_ASP.Models.Profile", b =>
                {
                    b.HasOne("_1U_ASP.Models.Person", "Person")
                        .WithMany("Profile")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_1U_ASP.Models.SaleOrder", b =>
                {
                    b.HasOne("_1U_ASP.Models.Shop", "Shop")
                        .WithMany("SaleOrders")
                        .HasForeignKey("ShopId");
                });

            modelBuilder.Entity("_1U_ASP.Models.SaleOrderDetail", b =>
                {
                    b.HasOne("Dap1U.Models.Product", "Product")
                        .WithMany("SaleOrderDetails")
                        .HasForeignKey("ProductId");

                    b.HasOne("_1U_ASP.Models.SaleOrder", "SaleOrder")
                        .WithMany("SaleOrderDetails")
                        .HasForeignKey("SaleOrderId");
                });

            modelBuilder.Entity("_1U_ASP.Models.SalePriseDoc", b =>
                {
                    b.HasOne("Dap1U.Models.Product", "Product")
                        .WithMany("SalePriseDocs")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("_1U_ASP.Models.ShopBalanceGood", b =>
                {
                    b.HasOne("_1U_ASP.Models.DocEnterProduct", "DocEnterProduct")
                        .WithMany("ShopBalanceGood")
                        .HasForeignKey("DocEnterProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dap1U.Models.Product", "Product")
                        .WithMany("ShopBalanceGood")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("_1U_ASP.Models.Shop", "Shop")
                        .WithMany("ShopBalanceGood")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_1U_ASP.Models.ShopProduct", b =>
                {
                    b.HasOne("Dap1U.Models.Product", "Product")
                        .WithMany("ShopProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("_1U_ASP.Models.Shop", "Shop")
                        .WithMany("ShopProducts")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppRoleClaim", b =>
                {
                    b.HasOne("_1U_ASP.Security.Model.AppRole", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppUser", b =>
                {
                    b.HasOne("_1U_ASP.Security.Model.AppPerson", "Person")
                        .WithMany("AspNetUsers")
                        .HasForeignKey("PersonId")
                        .HasConstraintName("FK_AspNetUsers_Person");
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppUserClaim", b =>
                {
                    b.HasOne("_1U_ASP.Security.Model.AppUser", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppUserLogin", b =>
                {
                    b.HasOne("_1U_ASP.Security.Model.AppUser", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppUserRole", b =>
                {
                    b.HasOne("_1U_ASP.Security.Model.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("_1U_ASP.Security.Model.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AppUserToken", b =>
                {
                    b.HasOne("_1U_ASP.Security.Model.AppUser", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_1U_ASP.Security.Model.AspNetUserClaimRole", b =>
                {
                    b.HasOne("_1U_ASP.Security.Model.AppUserClaim", "UserClaim")
                        .WithMany("UserClaimRoles")
                        .HasForeignKey("ClaimId")
                        .HasConstraintName("FK_AspNetUserClaimRole_AspNetUserClaims_UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("_1U_ASP.Security.Model.AppRole", "Role")
                        .WithMany("UserClaimRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
