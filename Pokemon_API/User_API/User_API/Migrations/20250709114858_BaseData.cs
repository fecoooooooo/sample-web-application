using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User_API.Migrations
{
    /// <inheritdoc />
    public partial class BaseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "TempUsers",
                newName: "NameJP");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "TempUsers",
                newName: "NameEN");

            migrationBuilder.InsertData(
                table: "TempUsers",
                columns: new[] { "Id", "Age", "NameEN", "NameJP" },
                values: new object[,]
                {
                    { 1, 10, "Ash", "Satoshi" },
                    { 2, 9, "Misty", "Kasumi" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TempUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TempUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "NameJP",
                table: "TempUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "NameEN",
                table: "TempUsers",
                newName: "FirstName");
        }
    }
}
