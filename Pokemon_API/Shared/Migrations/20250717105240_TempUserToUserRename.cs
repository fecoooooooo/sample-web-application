using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class TempUserToUserRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempUser");

            migrationBuilder.CreateTable(
                name: "User",
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
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
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
                name: "User");

            migrationBuilder.CreateTable(
                name: "TempUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    NameEN = table.Column<string>(type: "text", nullable: true),
                    NameJP = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempUser", x => x.Id);
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
    }
}
