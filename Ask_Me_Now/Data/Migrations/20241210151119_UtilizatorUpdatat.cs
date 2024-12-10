using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ask_Me_Now.Data.Migrations
{
    /// <inheritdoc />
    public partial class UtilizatorUpdatat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Categorii_CategorieId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CategorieId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CategorieId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CategoriePreferata",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategorieId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CategoriePreferata",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CategorieId",
                table: "AspNetUsers",
                column: "CategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Categorii_CategorieId",
                table: "AspNetUsers",
                column: "CategorieId",
                principalTable: "Categorii",
                principalColumn: "CategorieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
