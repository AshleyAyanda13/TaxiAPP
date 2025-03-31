using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiAppApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverDel_Driver_driverId",
                table: "DriverDel");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_bookingId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "StripeId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "bookingId",
                table: "Payments",
                newName: "BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_bookingId",
                table: "Payments",
                newName: "IX_Payments_BookingId");

            migrationBuilder.RenameColumn(
                name: "driverId",
                table: "DriverDel",
                newName: "DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverDel_driverId",
                table: "DriverDel",
                newName: "IX_DriverDel_DriverId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BookingDate",
                table: "Bookings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_DriverDel_Driver_DriverId",
                table: "DriverDel",
                column: "DriverId",
                principalTable: "Driver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverDel_Driver_DriverId",
                table: "DriverDel");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Payments",
                newName: "bookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                newName: "IX_Payments_bookingId");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "DriverDel",
                newName: "driverId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverDel_DriverId",
                table: "DriverDel",
                newName: "IX_DriverDel_driverId");

            migrationBuilder.AddColumn<string>(
                name: "StripeId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverDel_Driver_driverId",
                table: "DriverDel",
                column: "driverId",
                principalTable: "Driver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_bookingId",
                table: "Payments",
                column: "bookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
