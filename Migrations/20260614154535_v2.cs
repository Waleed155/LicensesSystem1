using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licenses.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "Lots");

            migrationBuilder.RenameColumn(
                name: "BuildingNumber",
                table: "Lots",
                newName: "UnitNumber");

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Lots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Lots");

            migrationBuilder.RenameColumn(
                name: "UnitNumber",
                table: "Lots",
                newName: "BuildingNumber");

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "Lots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
