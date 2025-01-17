using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalHouseSystemApi.Migrations
{
    public partial class CategoryPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Products_ProductId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ProductId",
                table: "Photos");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Photos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_CategoryId",
                table: "Photos",
                column: "CategoryId",
                unique: true,
                filter: "[CategoryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProductId",
                table: "Photos",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Categories_CategoryId",
                table: "Photos",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Products_ProductId",
                table: "Photos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Categories_CategoryId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Products_ProductId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_CategoryId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ProductId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Photos");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProductId",
                table: "Photos",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Products_ProductId",
                table: "Photos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
