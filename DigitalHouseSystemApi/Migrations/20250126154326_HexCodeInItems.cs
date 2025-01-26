using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalHouseSystemApi.Migrations
{
    public partial class HexCodeInItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HexCode",
                table: "ShoppingCartItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HexCode",
                table: "ShoppingCartItems");
        }
    }
}
