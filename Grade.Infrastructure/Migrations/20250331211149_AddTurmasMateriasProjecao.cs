using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grade.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTurmasMateriasProjecao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MateriasProjecao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    CargaHoraria = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriasProjecao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TurmasProjecao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Ano = table.Column<int>(type: "integer", nullable: false),
                    Turno = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasProjecao", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MateriasProjecao");

            migrationBuilder.DropTable(
                name: "TurmasProjecao");
        }
    }
}
