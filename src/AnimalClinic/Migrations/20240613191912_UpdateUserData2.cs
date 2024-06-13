using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalClinic.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Role" },
                values: new object[] { "AQAAAAIAAYagAAAAEBwWrGNGragW5/7dhSH+nYaOPGhD0cN5y2XNVHcY98ybQxqKLjTtGpuE3dcPB6BnFg==", "Admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "Role" },
                values: new object[] { "AQAAAAIAAYagAAAAEFm045cyyxD8Esz6ESkuwVMv6I+tVVmiuzszqbEWCAqdMfzDjMqFNqhKXIvM5/aYdw==", "User" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Roles" },
                values: new object[] { "AQAAAAIAAYagAAAAEA/0oRZosP58B3OIIMkurlPzFegi4LHisnAqsLc63zKGhR5rjnS3hUhoqPmZICVqwg==", "Admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "Roles" },
                values: new object[] { "AQAAAAIAAYagAAAAEJpyFfNDeC9j+v3RB/YCJuzgt8nrJ0qUnoKQvfsme7b4FpGKsKu6xUDJdK1hwJpysg==", "User" });
        }
    }
}
