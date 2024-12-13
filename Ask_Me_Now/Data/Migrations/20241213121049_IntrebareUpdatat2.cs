using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ask_Me_Now.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntrebareUpdatat2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intrebari_Categorii_CategorieID",
                table: "Intrebari");

            migrationBuilder.RenameColumn(
                name: "CategorieID",
                table: "Intrebari",
                newName: "CategorieId");

            migrationBuilder.RenameIndex(
                name: "IX_Intrebari_CategorieID",
                table: "Intrebari",
                newName: "IX_Intrebari_CategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Intrebari_Categorii_CategorieId",
                table: "Intrebari",
                column: "CategorieId",
                principalTable: "Categorii",
                principalColumn: "CategorieId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intrebari_Categorii_CategorieId",
                table: "Intrebari");

            migrationBuilder.RenameColumn(
                name: "CategorieId",
                table: "Intrebari",
                newName: "CategorieID");

            migrationBuilder.RenameIndex(
                name: "IX_Intrebari_CategorieId",
                table: "Intrebari",
                newName: "IX_Intrebari_CategorieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Intrebari_Categorii_CategorieID",
                table: "Intrebari",
                column: "CategorieID",
                principalTable: "Categorii",
                principalColumn: "CategorieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
