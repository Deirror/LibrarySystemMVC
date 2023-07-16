using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsConfrimed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22820884-bcfa-427e-9092-794b487b672c"));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsConfirmed", "Name", "Nickname", "Password", "Role" },
                values: new object[] { new Guid("ca170964-6330-4f05-a34e-35e6dc2280fe"), "admin@gmail.com", null, "Alex Angelow", "Admin", "ABuzqTpIUT18cGlTSeaj3bmLUxV+A5ptcGDuTB50XMfHbLw/aDhzfDhfbHpdEa9zGg==", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ca170964-6330-4f05-a34e-35e6dc2280fe"));

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Nickname", "Password", "Role" },
                values: new object[] { new Guid("22820884-bcfa-427e-9092-794b487b672c"), "admin@gmail.com", "Alex Angelow", "Admin", "AAQJl48p+YBlD2QgLIaA6EyBB+QlA+IvhUDFAxHGgmpvxy7JXHe2CopgT8mYa5zjFQ==", "Admin" });
        }
    }
}
