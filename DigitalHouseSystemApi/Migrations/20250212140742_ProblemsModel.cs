using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalHouseSystemApi.Migrations
{
    public partial class ProblemsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProblemId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Problems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Context = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProblemId",
                table: "Photos",
                column: "ProblemId",
                unique: true,
                filter: "[ProblemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Problems_ProblemId",
                table: "Photos",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Problems_ProblemId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ProblemId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ProblemId",
                table: "Photos");
        }
    }
}
