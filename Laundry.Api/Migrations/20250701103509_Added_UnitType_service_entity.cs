using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laundry.Api.Migrations
{
    /// <inheritdoc />
    public partial class Added_UnitType_service_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderCode",
                table: "Orders",
                newName: "SpecialInstructions");

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryType",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DropOffDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedReadyDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExpressCharge",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExpress",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PickupType",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UnitType",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "UnitType",
                value: null);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "UnitType",
                value: null);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "UnitType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CancelReason", "CancelledAt", "CustomerPhone", "DeliveryType", "DiscountAmount", "DropOffDate", "ExpectedReadyDate", "ExpressCharge", "IsCancelled", "IsExpress", "PaymentMethod", "PaymentStatus", "PickupType", "TaxAmount", "UpdatedAt" },
                values: new object[] { null, null, null, 0, 0m, null, null, 0m, false, false, 5, 0, 0, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CancelReason", "CancelledAt", "CustomerPhone", "DeliveryType", "DiscountAmount", "DropOffDate", "ExpectedReadyDate", "ExpressCharge", "IsCancelled", "IsExpress", "PaymentMethod", "PaymentStatus", "PickupType", "TaxAmount", "UpdatedAt" },
                values: new object[] { null, null, null, 0, 0m, null, null, 0m, false, false, 5, 0, 0, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CancelledAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DropOffDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ExpectedReadyDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ExpressCharge",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsExpress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PickupType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "SpecialInstructions",
                table: "Orders",
                newName: "OrderCode");
        }
    }
}
