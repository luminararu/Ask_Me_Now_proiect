using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ask_Me_Now.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProfilInUtilizator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Profiluri_ProfilId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Intrebari_Profiluri_ProfilId",
                table: "Intrebari");

            migrationBuilder.DropForeignKey(
                name: "FK_Raspunsuri_Profiluri_ProfilId",
                table: "Raspunsuri");

            migrationBuilder.DropTable(
                name: "Profiluri");

            migrationBuilder.DropIndex(
                name: "IX_Raspunsuri_ProfilId",
                table: "Raspunsuri");

            migrationBuilder.DropIndex(
                name: "IX_Intrebari_ProfilId",
                table: "Intrebari");

            migrationBuilder.RenameColumn(
                name: "ProfilId",
                table: "Raspunsuri",
                newName: "Nume");

            migrationBuilder.RenameColumn(
                name: "ProfilId",
                table: "Intrebari",
                newName: "Nume");

            migrationBuilder.RenameColumn(
                name: "ProfilId",
                table: "AspNetUsers",
                newName: "CategorieId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_ProfilId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CategorieId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoriePreferata",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Descriere",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nume",
                table: "AspNetUsers",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Categorii_CategorieId",
                table: "AspNetUsers",
                column: "CategorieId",
                principalTable: "Categorii",
                principalColumn: "CategorieId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Categorii_CategorieId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CategoriePreferata",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Descriere",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nume",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Nume",
                table: "Raspunsuri",
                newName: "ProfilId");

            migrationBuilder.RenameColumn(
                name: "Nume",
                table: "Intrebari",
                newName: "ProfilId");

            migrationBuilder.RenameColumn(
                name: "CategorieId",
                table: "AspNetUsers",
                newName: "ProfilId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CategorieId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_ProfilId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateTable(
                name: "Profiluri",
                columns: table => new
                {
                    ProfilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategorieId = table.Column<int>(type: "int", nullable: false),
                    CategoriePreferata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nume = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Raspunsuri_ProfilId",
                table: "Raspunsuri",
                column: "ProfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Intrebari_ProfilId",
                table: "Intrebari",
                column: "ProfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiluri_CategorieId",
                table: "Profiluri",
                column: "CategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Profiluri_ProfilId",
                table: "AspNetUsers",
                column: "ProfilId",
                principalTable: "Profiluri",
                principalColumn: "ProfilId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Intrebari_Profiluri_ProfilId",
                table: "Intrebari",
                column: "ProfilId",
                principalTable: "Profiluri",
                principalColumn: "ProfilId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Raspunsuri_Profiluri_ProfilId",
                table: "Raspunsuri",
                column: "ProfilId",
                principalTable: "Profiluri",
                principalColumn: "ProfilId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
