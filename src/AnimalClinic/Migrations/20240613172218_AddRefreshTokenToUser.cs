using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalClinic.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "RefreshToken" },
                values: new object[] { "AQAAAAIAAYagAAAAEArs8KzfOQu3eUXLARf9u62mcYQq/Dk00eE/Jqwa6nzf3dHREdESRsgTSA3gaO97eQ==", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "RefreshToken" },
                values: new object[] { "AQAAAAIAAYagAAAAEH6nsk/QqvSsQ3KJlQ5lI5GQ32563id8XaWILabwW2JfUESnGUlChKUntH0XIbMuHQ==", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEDJ0z1K84ncnvXCZVEy6j0r7RKMR5bZWL1bv5TofrEU+TiR74E3MVj0TFjOl4bGzw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEONRpWhfyMYqDA4cwbeQVK3JknFL3/LW89Pm8fUfEaa/sDZIx/GgjlSsv8cX4ZzQw==");
        }
    }
}
