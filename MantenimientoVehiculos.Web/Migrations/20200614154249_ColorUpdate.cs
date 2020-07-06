using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MantenimientoVehiculos.Web.Migrations
{
    public partial class ColorUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Country");

            migrationBuilder.RenameColumn(
                name: "FechaModificacion",
                table: "Country",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "FechaModificacion",
                table: "Colors",
                newName: "ModificationDate");

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Colors",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Country",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Country");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Country",
                newName: "FechaModificacion");

            migrationBuilder.RenameColumn(
                name: "ModificationDate",
                table: "Colors",
                newName: "FechaModificacion");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Colors",
                newName: "FechaCreacion");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Country",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
