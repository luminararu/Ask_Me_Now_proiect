using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ask_Me_Now.Data.Migrations
{
    /// <inheritdoc />
    public partial class Categorii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumarIntrebari",
                table: "Categorii");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumarIntrebari",
                table: "Categorii",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
