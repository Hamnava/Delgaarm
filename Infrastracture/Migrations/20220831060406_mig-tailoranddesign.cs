using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Migrations
{
    public partial class migtailoranddesign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TailorSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shane = table.Column<float>(type: "real", nullable: false),
                    Astin = table.Column<float>(type: "real", nullable: false),
                    Kamar = table.Column<float>(type: "real", nullable: false),
                    ZerBaghal = table.Column<float>(type: "real", nullable: false),
                    Shelwar = table.Column<float>(type: "real", nullable: false),
                    perahan = table.Column<float>(type: "real", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailorSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TailorSizes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YourDesigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YourDesigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YourDesigns_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TailorSizes_UserId",
                table: "TailorSizes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_YourDesigns_UserId",
                table: "YourDesigns",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TailorSizes");

            migrationBuilder.DropTable(
                name: "YourDesigns");
        }
    }
}
