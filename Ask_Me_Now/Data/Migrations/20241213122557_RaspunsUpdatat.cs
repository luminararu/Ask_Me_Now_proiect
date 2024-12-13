using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ask_Me_Now.Data.Migrations
{
    /// <inheritdoc />
    public partial class RaspunsUpdatat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Raspunsuri_Intrebari_IntrebareId",
                table: "Raspunsuri");

            migrationBuilder.DropColumn(
                name: "Nume",
                table: "Raspunsuri");

            migrationBuilder.AlterColumn<int>(
                name: "IntrebareId",
                table: "Raspunsuri",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Raspunsuri_Intrebari_IntrebareId",
                table: "Raspunsuri",
                column: "IntrebareId",
                principalTable: "Intrebari",
                principalColumn: "IntrebareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Raspunsuri_Intrebari_IntrebareId",
                table: "Raspunsuri");

            migrationBuilder.AlterColumn<int>(
                name: "IntrebareId",
                table: "Raspunsuri",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nume",
                table: "Raspunsuri",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Raspunsuri_Intrebari_IntrebareId",
                table: "Raspunsuri",
                column: "IntrebareId",
                principalTable: "Intrebari",
                principalColumn: "IntrebareId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
