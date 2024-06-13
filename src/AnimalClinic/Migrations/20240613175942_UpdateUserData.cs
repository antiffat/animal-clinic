using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalClinic.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "PasswordHash", "Roles", "Username" },
                values: new object[] { "AQAAAAIAAYagAAAAEA/0oRZosP58B3OIIMkurlPzFegi4LHisnAqsLc63zKGhR5rjnS3hUhoqPmZICVqwg==", "Admin", "admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "Roles", "Username" },
                values: new object[] { "AQAAAAIAAYagAAAAEJpyFfNDeC9j+v3RB/YCJuzgt8nrJ0qUnoKQvfsme7b4FpGKsKu6xUDJdK1hwJpysg==", "User", "testuser" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "AQAAAAIAAYagAAAAEArs8KzfOQu3eUXLARf9u62mcYQq/Dk00eE/Jqwa6nzf3dHREdESRsgTSA3gaO97eQ==", "testuser1" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "AQAAAAIAAYagAAAAEH6nsk/QqvSsQ3KJlQ5lI5GQ32563id8XaWILabwW2JfUESnGUlChKUntH0XIbMuHQ==", "testuser2" });
        }
    }
}
