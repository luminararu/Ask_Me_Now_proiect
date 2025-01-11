using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ask_Me_Now.Data.Migrations
{
    /// <inheritdoc />
    public partial class Interactiuni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UtilizatorInteractiune",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilizatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RaspunsId = table.Column<int>(type: "int", nullable: false),
                    Liked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilizatorInteractiune", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UtilizatorInteractiune_AspNetUsers_UtilizatorId",
                        column: x => x.UtilizatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UtilizatorInteractiune_Raspunsuri_RaspunsId",
                        column: x => x.RaspunsId,
                        principalTable: "Raspunsuri",
                        principalColumn: "RaspunsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UtilizatorInteractiune_RaspunsId",
                table: "UtilizatorInteractiune",
                column: "RaspunsId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilizatorInteractiune_UtilizatorId",
                table: "UtilizatorInteractiune",
                column: "UtilizatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UtilizatorInteractiune");
        }
    }
}
