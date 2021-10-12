using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsSite.Migrations
{
    public partial class AddedFiELDForCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnPreview",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnPreview",
                table: "Product");
        }
    }
}
