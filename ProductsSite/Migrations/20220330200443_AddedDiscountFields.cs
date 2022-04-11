using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsSite.Migrations
{
    public partial class AddedDiscountFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountPercent",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscount",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsDiscount",
                table: "Product");
        }
    }
}
