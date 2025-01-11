using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ask_Me_Now.Data.Migrations
{
    /// <inheritdoc />
    public partial class PozaProfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PozaProfil",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PozaProfil",
                table: "AspNetUsers");
        }
    }
}
