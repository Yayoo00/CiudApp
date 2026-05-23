using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CiudApp.Migrations
{
    /// <inheritdoc />
    public partial class CrearPerfilUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PerfilesUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Universidad = table.Column<string>(type: "TEXT", nullable: false),
                    Distrito = table.Column<string>(type: "TEXT", nullable: false),
                    Nivel = table.Column<string>(type: "TEXT", nullable: false),
                    Puntos = table.Column<int>(type: "INTEGER", nullable: false),
                    RutasCompletadas = table.Column<int>(type: "INTEGER", nullable: false),
                    RetosCompletados = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilesUsuario", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfilesUsuario");
        }
    }
}
