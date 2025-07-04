using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laundry.Api.Migrations
{
    /// <inheritdoc />
    public partial class servictableupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerKg",
                table: "Services",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1,
                column: "Unit",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2,
                column: "Unit",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3,
                column: "Unit",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Services",
                newName: "PricePerKg");
        }
    }
}
