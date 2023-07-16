using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIsConfirmed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ca170964-6330-4f05-a34e-35e6dc2280fe"));

            migrationBuilder.AlterColumn<string>(
                name: "IsConfirmed",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsConfirmed", "Name", "Nickname", "Password", "Role" },
                values: new object[] { new Guid("80df61e3-6c9b-46ce-849f-4c1654b28cf0"), "admin@gmail.com", "Yes", "Alex Angelow", "Admin", "ALtweqtGPMZB6EC1E7wXdUrfnrcVk74Re1rkvmHX2owHF+ost8oUr2KTvfI5EoYrjg==", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("80df61e3-6c9b-46ce-849f-4c1654b28cf0"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsConfirmed",
                table: "Users",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsConfirmed", "Name", "Nickname", "Password", "Role" },
                values: new object[] { new Guid("ca170964-6330-4f05-a34e-35e6dc2280fe"), "admin@gmail.com", null, "Alex Angelow", "Admin", "ABuzqTpIUT18cGlTSeaj3bmLUxV+A5ptcGDuTB50XMfHbLw/aDhzfDhfbHpdEa9zGg==", "Admin" });
        }
    }
}
