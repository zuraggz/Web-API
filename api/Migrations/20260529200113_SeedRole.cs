using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32d8bc93-3af4-4b8c-bcb4-9ec9d56151d5", "8b46b674-1de1-430d-9ea9-762074865e46", "Admin", "ADMIN" },
                    { "77306733-78f5-4cff-b8b4-b1924259c0da", "7037fdc9-b46c-41f8-9826-ab4ed19cd38c", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32d8bc93-3af4-4b8c-bcb4-9ec9d56151d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77306733-78f5-4cff-b8b4-b1924259c0da");

        }
    }
}
