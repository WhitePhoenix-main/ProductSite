using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsSite.Migrations
{
    public partial class RenamedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Product",
                newName: "ProductType");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Product",
                newName: "ProductName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProductDate",
                table: "Product",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductType",
                table: "Product",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Product",
                newName: "Name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProductDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
