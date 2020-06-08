using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GildedRose.Web.Migrations
{
    public partial class datetimeoffset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("de60a276-cc1d-4dfe-aab5-057f0b63fad6"),
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 6, 7, 3, 54, 14, 291, DateTimeKind.Unspecified).AddTicks(9746), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TransactionDate",
                table: "Transactions",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("de60a276-cc1d-4dfe-aab5-057f0b63fad6"),
                column: "DateCreated",
                value: new DateTimeOffset(new DateTime(2020, 6, 6, 20, 43, 31, 516, DateTimeKind.Unspecified).AddTicks(7178), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
