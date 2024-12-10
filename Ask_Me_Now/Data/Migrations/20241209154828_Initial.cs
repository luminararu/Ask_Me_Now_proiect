using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ask_Me_Now.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfilId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categorii",
                columns: table => new
                {
                    CategorieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumarIntrebari = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorii", x => x.CategorieId);
                });

            migrationBuilder.CreateTable(
                name: "Profiluri",
                columns: table => new
                {
                    ProfilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoriePreferata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategorieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiluri", x => x.ProfilId);
                    table.ForeignKey(
                        name: "FK_Profiluri_Categorii_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categorii",
                        principalColumn: "CategorieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Intrebari",
                columns: table => new
                {
                    IntrebareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategorieID = table.Column<int>(type: "int", nullable: false),
                    ProfilId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Continut = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false),
                    UtilizatorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intrebari", x => x.IntrebareId);
                    table.ForeignKey(
                        name: "FK_Intrebari_AspNetUsers_UtilizatorId",
                        column: x => x.UtilizatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Intrebari_Categorii_CategorieID",
                        column: x => x.CategorieID,
                        principalTable: "Categorii",
                        principalColumn: "CategorieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Intrebari_Profiluri_ProfilId",
                        column: x => x.ProfilId,
                        principalTable: "Profiluri",
                        principalColumn: "ProfilId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Raspunsuri",
                columns: table => new
                {
                    RaspunsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfilId = table.Column<int>(type: "int", nullable: false),
                    IntrebareId = table.Column<int>(type: "int", nullable: false),
                    Continut = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false),
                    UtilizatorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raspunsuri", x => x.RaspunsId);
                    table.ForeignKey(
                        name: "FK_Raspunsuri_AspNetUsers_UtilizatorId",
                        column: x => x.UtilizatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Raspunsuri_Intrebari_IntrebareId",
                        column: x => x.IntrebareId,
                        principalTable: "Intrebari",
                        principalColumn: "IntrebareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Raspunsuri_Profiluri_ProfilId",
                        column: x => x.ProfilId,
                        principalTable: "Profiluri",
                        principalColumn: "ProfilId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfilId",
                table: "AspNetUsers",
                column: "ProfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Intrebari_CategorieID",
                table: "Intrebari",
                column: "CategorieID");

            migrationBuilder.CreateIndex(
                name: "IX_Intrebari_ProfilId",
                table: "Intrebari",
                column: "ProfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Intrebari_UtilizatorId",
                table: "Intrebari",
                column: "UtilizatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiluri_CategorieId",
                table: "Profiluri",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Raspunsuri_IntrebareId",
                table: "Raspunsuri",
                column: "IntrebareId");

            migrationBuilder.CreateIndex(
                name: "IX_Raspunsuri_ProfilId",
                table: "Raspunsuri",
                column: "ProfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Raspunsuri_UtilizatorId",
                table: "Raspunsuri",
                column: "UtilizatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Profiluri_ProfilId",
                table: "AspNetUsers",
                column: "ProfilId",
                principalTable: "Profiluri",
                principalColumn: "ProfilId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Profiluri_ProfilId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Raspunsuri");

            migrationBuilder.DropTable(
                name: "Intrebari");

            migrationBuilder.DropTable(
                name: "Profiluri");

            migrationBuilder.DropTable(
                name: "Categorii");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfilId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilId",
                table: "AspNetUsers");
        }
    }
}
