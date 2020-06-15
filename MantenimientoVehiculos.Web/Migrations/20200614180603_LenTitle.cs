using Microsoft.EntityFrameworkCore.Migrations;

namespace MantenimientoVehiculos.Web.Migrations
{
    public partial class LenTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "JobTitle",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "JobTitle",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
