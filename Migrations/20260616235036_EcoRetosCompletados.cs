using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CiudApp.Migrations
{
    /// <inheritdoc />
    public partial class EcoRetosCompletados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EcoRetosCompletados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    NombreReto = table.Column<string>(type: "TEXT", nullable: false),
                    FechaCompletado = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcoRetosCompletados", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EcoRetosCompletados");
        }
    }
}
