using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaReservasLabs.Migrations
{
    /// <inheritdoc />
    public partial class AddEmManutencaoToLaboratorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmManutencao",
                table: "Laboratorios",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmManutencao",
                table: "Laboratorios");
        }
    }
}
