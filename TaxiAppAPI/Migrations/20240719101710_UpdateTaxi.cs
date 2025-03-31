using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiAppApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaxi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Taxi",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "Taxi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Taxi");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Taxi",
                newName: "Model");
        }
    }
}
