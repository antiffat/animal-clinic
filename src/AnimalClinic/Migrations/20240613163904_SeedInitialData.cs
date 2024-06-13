using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AnimalClinic.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, "AQAAAAIAAYagAAAAEEDJ0z1K84ncnvXCZVEy6j0r7RKMR5bZWL1bv5TofrEU+TiR74E3MVj0TFjOl4bGzw==", "testuser1" },
                    { 2, "AQAAAAIAAYagAAAAEEONRpWhfyMYqDA4cwbeQVK3JknFL3/LW89Pm8fUfEaa/sDZIx/GgjlSsv8cX4ZzQw==", "testuser2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
