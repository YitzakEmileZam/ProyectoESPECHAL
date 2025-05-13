using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pg1.Migrations
{
    /// <inheritdoc />
    public partial class agregarcontra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "contrasena",
                table: "Clientes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contrasena",
                table: "Clientes");
        }
    }
}
