using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalHouseSystemApi.Migrations
{
    public partial class Tariffs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternetPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternetSpeed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversationTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MagentaTV = table.Column<int>(type: "int", nullable: false),
                    MagentaTV_GO = table.Column<bool>(type: "bit", nullable: false),
                    Functions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternetPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TariffTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TariffCategory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Magenta1s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InternetPackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magenta1s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Magenta1s_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Magenta1s_InternetPackages_InternetPackageId",
                        column: x => x.InternetPackageId,
                        principalTable: "InternetPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternetSpeed = table.Column<int>(type: "int", nullable: false),
                    ConversationTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SMS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoamingInternet = table.Column<int>(type: "int", nullable: true),
                    InternationalNetworkCalls = table.Column<int>(type: "int", nullable: true),
                    E_bill = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TariffTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tariffs_TariffTypes_TariffTypeId",
                        column: x => x.TariffTypeId,
                        principalTable: "TariffTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Magenta1Tariffs",
                columns: table => new
                {
                    Magenta1Id = table.Column<int>(type: "int", nullable: false),
                    TariffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magenta1Tariffs", x => new { x.TariffId, x.Magenta1Id });
                    table.ForeignKey(
                        name: "FK_Magenta1Tariffs_Magenta1s_Magenta1Id",
                        column: x => x.Magenta1Id,
                        principalTable: "Magenta1s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Magenta1Tariffs_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Magenta1s_InternetPackageId",
                table: "Magenta1s",
                column: "InternetPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Magenta1Tariffs_Magenta1Id",
                table: "Magenta1Tariffs",
                column: "Magenta1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_TariffTypeId",
                table: "Tariffs",
                column: "TariffTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Magenta1Tariffs");

            migrationBuilder.DropTable(
                name: "Magenta1s");

            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.DropTable(
                name: "InternetPackages");

            migrationBuilder.DropTable(
                name: "TariffTypes");
        }
    }
}
