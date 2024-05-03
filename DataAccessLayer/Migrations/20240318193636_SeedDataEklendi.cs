using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "Email", "IsActive", "IsAdmin", "Name", "Password", "Phone", "RefreshToken", "RefreshTokenExpireDate", "Surname", "UserGuid" },
                values: new object[] { 1, new DateTime(2024, 3, 18, 22, 36, 35, 464, DateTimeKind.Local).AddTicks(3019), "admin@mvckurumsal.net", true, true, "Admin", "Admin123", null, null, null, "User", "3740fc38-0746-4c8d-b1e8-19f07c166408" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
