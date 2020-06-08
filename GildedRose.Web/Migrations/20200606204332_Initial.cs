﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GildedRose.Web.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Base64ImageContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ItemNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StockCount = table.Column<int>(nullable: false),
                    UnitCost = table.Column<double>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    UserId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
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
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ImageFileId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_ImageFiles_ImageFileId",
                        column: x => x.ImageFileId,
                        principalTable: "ImageFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TransactionDate = table.Column<DateTimeOffset>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ImageFiles",
                columns: new[] { "Id", "Base64ImageContent" },
                values: new object[] { new Guid("de62a276-cc1d-4dfe-aab5-057f0b63fad6"), "data:image/jpeg;base64,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" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateCreated", "Description", "ItemNumber", "Name", "StockCount", "UnitCost" },
                values: new object[] { new Guid("de60a276-cc1d-4dfe-aab5-057f0b63fad6"), new DateTimeOffset(new DateTime(2020, 6, 6, 20, 43, 31, 516, DateTimeKind.Unspecified).AddTicks(7178), new TimeSpan(0, 0, 0, 0, 0)), "This Golden Diadem can be worn for many occasions, such as fairs and festivals.", "0111022342340", "Gold Diadem", 27, 29.989999999999998 });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageFileId", "Order", "ProductId" },
                values: new object[] { new Guid("de61a276-cc1d-4dfe-aab5-057f0b63fad6"), new Guid("de62a276-cc1d-4dfe-aab5-057f0b63fad6"), 1, new Guid("de60a276-cc1d-4dfe-aab5-057f0b63fad6") });

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
                name: "IX_ProductImages_ImageFileId",
                table: "ProductImages",
                column: "ImageFileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ProductId",
                table: "Transactions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ImageFiles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
