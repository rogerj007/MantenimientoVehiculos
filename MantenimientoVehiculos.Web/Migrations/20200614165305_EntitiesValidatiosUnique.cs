using Microsoft.EntityFrameworkCore.Migrations;

namespace MantenimientoVehiculos.Web.Migrations
{
    public partial class EntitiesValidatiosUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VehicleStatus_VehicleStatus",
                table: "VehicleStatus",
                column: "VehicleStatus",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleBrand_VehicleBrand",
                table: "VehicleBrand",
                column: "VehicleBrand",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleType_VehicleType",
                table: "VehicleType",
                column: "VehicleType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobTitle_JobTitle",
                table: "JobTitle",
                column: "JobTitle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fuel_Fuel",
                table: "Fuel",
                column: "Fuel",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleStatus_VehicleStatus",
                table: "VehicleStatus");

            migrationBuilder.DropIndex(
                name: "IX_VehicleBrand_VehicleBrand",
                table: "VehicleBrand");

            migrationBuilder.DropIndex(
                name: "IX_VehicleType_VehicleType",
                table: "VehicleType");

            migrationBuilder.DropIndex(
                name: "IX_JobTitle_JobTitle",
                table: "JobTitle");

            migrationBuilder.DropIndex(
                name: "IX_Fuel_Fuel",
                table: "Fuel");
        }
    }
}
