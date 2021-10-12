using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsSite.Migrations
{
    public partial class AddedFiELDForCheck8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnPreview",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnPreview",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
