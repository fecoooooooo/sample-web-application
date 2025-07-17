using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameEN = table.Column<string>(type: "text", nullable: true),
                    NameJP = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempUser", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Pokemon",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Pikachu", "Electric" },
                    { 2, "Zapdosh", "Electric" },
                    { 3, "Charizard", "Fire" },
                    { 4, "Arcaine", "Fire" },
                    { 5, "Rapidash", "Fire" },
                    { 6, "Weedle", "Bug" },
                    { 7, "Venomoth", "Bug" },
                    { 8, "Metapod", "Bug" }
                });

            migrationBuilder.InsertData(
                table: "TempUser",
                columns: new[] { "Id", "Age", "NameEN", "NameJP" },
                values: new object[,]
                {
                    { 1, 10, "Ash", "Satoshi" },
                    { 2, 11, "Misty", "Kasumi" },
                    { 3, 11, "Brock", "Takeshi" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pokemon");

            migrationBuilder.DropTable(
                name: "TempUser");
        }
    }
}
