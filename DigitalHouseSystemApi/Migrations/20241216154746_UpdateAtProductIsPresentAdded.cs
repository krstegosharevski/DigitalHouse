using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalHouseSystemApi.Migrations
{
    public partial class UpdateAtProductIsPresentAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPresent",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPresent",
                table: "Products");
        }
    }
}
