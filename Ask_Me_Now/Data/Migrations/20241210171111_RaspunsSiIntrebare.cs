using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ask_Me_Now.Data.Migrations
{
    /// <inheritdoc />
    public partial class RaspunsSiIntrebare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Raspunsuri",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Raspunsuri",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Intrebari",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Raspunsuri");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Raspunsuri");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Intrebari");
        }
    }
}
