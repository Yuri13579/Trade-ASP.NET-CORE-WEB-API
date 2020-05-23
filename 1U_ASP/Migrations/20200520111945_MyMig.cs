using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _1U_ASP.Migrations
{
    public partial class MyMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    GenderSysCodeUniqueId = table.Column<int>(nullable: true),
                    Dob = table.Column<DateTime>(nullable: true),
                    UserActionId = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    PriceCost = table.Column<double>(nullable: false),
                    PriseSale = table.Column<double>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    UserActionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    ProviderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<decimal>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    UserActionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.ProviderId);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    ShopId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    UserActionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.ShopId);
                });

            migrationBuilder.CreateTable(
                name: "SysCode",
                columns: table => new
                {
                    SysCodeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueID = table.Column<int>(nullable: false),
                    SysCode = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    CodeID = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    OrderID = table.Column<int>(nullable: false),
                    ShortDesc = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    LongDesc = table.Column<string>(unicode: false, nullable: true),
                    Comments = table.Column<string>(unicode: false, nullable: true),
                    Group1 = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    Group2 = table.Column<bool>(nullable: false),
                    Group3 = table.Column<bool>(nullable: false),
                    Group4 = table.Column<bool>(nullable: false),
                    Group5 = table.Column<bool>(nullable: false),
                    Group6 = table.Column<bool>(nullable: false),
                    AddDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    AddUser = table.Column<string>(unicode: false, maxLength: 255, nullable: false, defaultValueSql: "(suser_sname())"),
                    ChangeDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ChangeUser = table.Column<string>(unicode: false, maxLength: 255, nullable: false, defaultValueSql: "(suser_sname())"),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysCode", x => x.SysCodeID);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "Core",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 40, nullable: false),
                    LastName = table.Column<string>(maxLength: 70, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 100, nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 90, nullable: false),
                    MobilePhone = table.Column<string>(maxLength: 20, nullable: true),
                    GenderSysCodeUniqueID = table.Column<int>(nullable: true),
                    DOB = table.Column<DateTime>(type: "date", nullable: true),
                    UserActionID = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    ProfileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    ProfileTypeSysCodeUniqueId = table.Column<int>(nullable: false),
                    ProfileTitle = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    UserActionId = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.ProfileId);
                    table.ForeignKey(
                        name: "FK_Profile_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalePriseDocs",
                columns: table => new
                {
                    SalePriseDocId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: true),
                    SalePrise = table.Column<decimal>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    UserActionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalePriseDocs", x => x.SalePriseDocId);
                    table.ForeignKey(
                        name: "FK_SalePriseDocs_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocEnterProducts",
                columns: table => new
                {
                    DocEnterProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProviderId = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    UserActionId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocEnterProducts", x => x.DocEnterProductId);
                    table.ForeignKey(
                        name: "FK_DocEnterProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocEnterProducts_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrders",
                columns: table => new
                {
                    SaleOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<int>(nullable: true),
                    DataTime = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    UserActionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrders", x => x.SaleOrderId);
                    table.ForeignKey(
                        name: "FK_SaleOrders_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopProduct",
                columns: table => new
                {
                    ShopProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    UserActionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopProduct", x => x.ShopProductId);
                    table.ForeignKey(
                        name: "FK_ShopProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopProduct_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Person",
                        column: x => x.PersonId,
                        principalSchema: "Core",
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocEnterProductDetails",
                columns: table => new
                {
                    DocEnterProductDetailId = table.Column<int>(nullable: false),
                    DocEnterProductId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    InPrise = table.Column<float>(nullable: false),
                    Summ = table.Column<double>(nullable: false),
                    Delete = table.Column<bool>(nullable: false),
                    UserActionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocEnterProductDetails", x => x.DocEnterProductDetailId);
                    table.ForeignKey(
                        name: "FK_DocEnterProductDetails_DocEnterProducts_DocEnterProductDetailId",
                        column: x => x.DocEnterProductDetailId,
                        principalTable: "DocEnterProducts",
                        principalColumn: "DocEnterProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocEnterProductDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopBalanceGood",
                columns: table => new
                {
                    ShopBalanceGoodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    DocEnterProductId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    UserActionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopBalanceGood", x => x.ShopBalanceGoodId);
                    table.ForeignKey(
                        name: "FK_ShopBalanceGood_DocEnterProducts_DocEnterProductId",
                        column: x => x.DocEnterProductId,
                        principalTable: "DocEnterProducts",
                        principalColumn: "DocEnterProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopBalanceGood_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopBalanceGood_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderDetails",
                columns: table => new
                {
                    SaleOrderDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SaleOrderId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    PriceCost = table.Column<double>(nullable: true),
                    PriseSale = table.Column<double>(nullable: true),
                    Summ = table.Column<double>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    UserActionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderDetails", x => x.SaleOrderDetailId);
                    table.ForeignKey(
                        name: "FK_SaleOrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrderDetails_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "SaleOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    ClaimProperty = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaimRole",
                columns: table => new
                {
                    AspNetUserClaimRoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimId = table.Column<int>(nullable: false),
                    RoleId = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaimRole", x => x.AspNetUserClaimRoleID);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaimRole_AspNetUserClaims_UserId",
                        column: x => x.ClaimId,
                        principalTable: "AspNetUserClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaimRole_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9cb615ae-3ddc-40da-8d03-65216fe22377", "3f827b0d-75fa-4a2e-99c1-540603ca5b54", "superadmin", "SUPERADMIN" },
                    { "5bb27bb1-913a-4516-92b6-fd8f667275aa", "8e193d2e-2a2a-45b1-ae7e-92c28af243ff", "companymanager", "COMPANYMANAGER" },
                    { "17d7983c-96e8-4d54-b51c-7692feb9a595", "a051d63a-e8a3-4bdf-b60e-2c337f023d19", "customeradminmanager", "CUSTOMERADMINMANAGER" },
                    { "13c4dd37-fe45-4d87-8926-5662547cf1b2", "edf487c0-47c6-4cbf-8fae-316a52b7c7e8", "jobseeker", "JOBSEEKER" },
                    { "0f4e8d62-a445-4626-b2c2-d1fc566a7184", "223bf724-59cb-405c-aebe-f7ba30d61eb0", "employee", "EMPLOYEE" },
                    { "0f26382a-ec94-401e-aef8-e2c44b376858", "e6b70a9b-1e2f-48d6-aecc-a31748bdd35f", "companyadmin", "COMPANYADMIN" },
                    { "9367245f-714a-4843-a58d-276e8200ddf4", "20a5aed0-6fc7-49b5-96ea-2a62c3cb17b6", "companyowner", "COMPANYOWNER" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Description", "Name", "PriceCost", "PriseSale", "UserActionId" },
                values: new object[] { 1, 4802221111.0, 0, "500ml", "Coca-Cola", 4.0, 5.0, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Description", "Name", "PriceCost", "PriseSale", "UserActionId" },
                values: new object[] { 6, 4802221335.0, 0, "333ml", "Mirinda", 3.0, 4.0, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Description", "Name", "PriceCost", "PriseSale", "UserActionId" },
                values: new object[] { 5, 4802221335.0, 0, "333ml", "Pepsi", 3.5, 4.5, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Description", "Name", "PriceCost", "PriseSale", "UserActionId" },
                values: new object[] { 4, 4802221334.0, 0, "500ml", "Schweppes", 4.0, 5.0, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Description", "Name", "PriceCost", "PriseSale", "UserActionId" },
                values: new object[] { 3, 4802221333.0, 0, "500ml", "Fanta", 4.0, 4.0, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Description", "Name", "PriceCost", "PriseSale", "UserActionId" },
                values: new object[] { 2, 4802221222.0, 0, "500ml", "Sprite", 3.0, 4.0, null });

            migrationBuilder.InsertData(
                table: "Provider",
                columns: new[] { "ProviderId", "Address", "Name", "Phone", "UserActionId" },
                values: new object[] { 1, "Shevchenko stree 34", "The Coca-Cola Company", 5241234123m, null });

            migrationBuilder.InsertData(
                table: "Provider",
                columns: new[] { "ProviderId", "Address", "Name", "Phone", "UserActionId" },
                values: new object[] { 2, "Hrushevskoho Street 112", "PepsiCo", 32121234123m, null });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "ShopId", "Address", "Name", "UserActionId" },
                values: new object[] { 2, "Ukrainska street 31 ", "EcoShop2", null });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "ShopId", "Address", "Name", "UserActionId" },
                values: new object[] { 1, "Virmenska street 100", "EcoShop1", null });

            migrationBuilder.InsertData(
                table: "SaleOrders",
                columns: new[] { "SaleOrderId", "DataTime", "ShopId", "UserActionId" },
                values: new object[] { 1, new DateTime(2020, 5, 20, 14, 19, 44, 831, DateTimeKind.Local).AddTicks(6074), 1, null });

            migrationBuilder.InsertData(
                table: "SaleOrders",
                columns: new[] { "SaleOrderId", "DataTime", "ShopId", "UserActionId" },
                values: new object[] { 2, new DateTime(2020, 5, 20, 14, 19, 44, 834, DateTimeKind.Local).AddTicks(1867), 2, null });

            migrationBuilder.InsertData(
                table: "ShopProduct",
                columns: new[] { "ShopProductId", "ProductId", "ShopId", "UserActionId" },
                values: new object[] { 1, 1, 1, null });

            migrationBuilder.InsertData(
                table: "ShopProduct",
                columns: new[] { "ShopProductId", "ProductId", "ShopId", "UserActionId" },
                values: new object[] { 2, 2, 1, null });

            migrationBuilder.InsertData(
                table: "ShopProduct",
                columns: new[] { "ShopProductId", "ProductId", "ShopId", "UserActionId" },
                values: new object[] { 3, 3, 1, null });

            migrationBuilder.InsertData(
                table: "ShopProduct",
                columns: new[] { "ShopProductId", "ProductId", "ShopId", "UserActionId" },
                values: new object[] { 4, 1, 2, null });

            migrationBuilder.InsertData(
                table: "ShopProduct",
                columns: new[] { "ShopProductId", "ProductId", "ShopId", "UserActionId" },
                values: new object[] { 5, 2, 2, null });

            migrationBuilder.InsertData(
                table: "ShopProduct",
                columns: new[] { "ShopProductId", "ProductId", "ShopId", "UserActionId" },
                values: new object[] { 6, 3, 2, null });

            migrationBuilder.InsertData(
                table: "SaleOrderDetails",
                columns: new[] { "SaleOrderDetailId", "Count", "Deleted", "PriceCost", "PriseSale", "ProductId", "SaleOrderId", "Summ", "UserActionId" },
                values: new object[,]
                {
                    { 1, 3, false, 12.5, 15.0, 1, 1, 45.0, null },
                    { 2, 2, false, 14.300000000000001, 17.0, 2, 1, 34.0, null },
                    { 3, 5, false, 12.5, 15.0, 1, 2, 75.0, null },
                    { 4, 4, false, 14.300000000000001, 17.0, 2, 2, 68.0, null },
                    { 5, 1, false, 16.0, 19.5, 3, 2, 19.5, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaimRole_ClaimId",
                table: "AspNetUserClaimRole",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaimRole_RoleId",
                table: "AspNetUserClaimRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonId",
                table: "AspNetUsers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DocEnterProductDetails_ProductId",
                table: "DocEnterProductDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DocEnterProducts_ProductId",
                table: "DocEnterProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DocEnterProducts_ProviderId",
                table: "DocEnterProducts",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_PersonId",
                table: "Profile",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetails_ProductId",
                table: "SaleOrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderDetails_SaleOrderId",
                table: "SaleOrderDetails",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_ShopId",
                table: "SaleOrders",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_SalePriseDocs_ProductId",
                table: "SalePriseDocs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopBalanceGood_DocEnterProductId",
                table: "ShopBalanceGood",
                column: "DocEnterProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopBalanceGood_ProductId",
                table: "ShopBalanceGood",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopBalanceGood_ShopId",
                table: "ShopBalanceGood",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopProduct_ProductId",
                table: "ShopProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopProduct_ShopId",
                table: "ShopProduct",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueID",
                table: "SysCode",
                column: "UniqueID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "nci_wi_SysCode_A450FA7C09A5ABDEA56D664EF1C878CA",
                table: "SysCode",
                columns: new[] { "Deleted", "ShortDesc" });

            migrationBuilder.CreateIndex(
                name: "nci_wi_SysCode_2F628BBB1C7C43B1E8662FE58A7B34AC",
                table: "SysCode",
                columns: new[] { "Deleted", "SysCode", "CodeID" });

            migrationBuilder.CreateIndex(
                name: "UQ__Person__49A147407F7850CA",
                schema: "Core",
                table: "Person",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.Sql(@"create proc [dbo].[spGetAllProducts] 
                                  AS SELECT * FROM [dbo].[Products]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaimRole");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DocEnterProductDetails");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "SaleOrderDetails");

            migrationBuilder.DropTable(
                name: "SalePriseDocs");

            migrationBuilder.DropTable(
                name: "ShopBalanceGood");

            migrationBuilder.DropTable(
                name: "ShopProduct");

            migrationBuilder.DropTable(
                name: "SysCode");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "SaleOrders");

            migrationBuilder.DropTable(
                name: "DocEnterProducts");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "Core");
        }
    }
}
