using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grade.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlunosProjecao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosProjecao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeHorarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Bimestre = table.Column<int>(type: "integer", nullable: false),
                    DiaSemana = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeHorarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MateriasProjecao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriasProjecao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfessoresProjecao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessoresProjecao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TurmasProjecao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Ano = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasProjecao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeHorarioMaterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MateriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfessorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeHorarioMaterias", x => new { x.Id, x.ProfessorId, x.MateriaId });
                    table.ForeignKey(
                        name: "FK_GradeHorarioMaterias_GradeHorarios_Id",
                        column: x => x.Id,
                        principalTable: "GradeHorarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeHorarioMaterias_MateriasProjecao_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "MateriasProjecao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeHorarioMaterias_ProfessoresProjecao_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "ProfessoresProjecao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradeHorarioMaterias_MateriaId",
                table: "GradeHorarioMaterias",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeHorarioMaterias_ProfessorId",
                table: "GradeHorarioMaterias",
                column: "ProfessorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunosProjecao");

            migrationBuilder.DropTable(
                name: "GradeHorarioMaterias");

            migrationBuilder.DropTable(
                name: "TurmasProjecao");

            migrationBuilder.DropTable(
                name: "GradeHorarios");

            migrationBuilder.DropTable(
                name: "MateriasProjecao");

            migrationBuilder.DropTable(
                name: "ProfessoresProjecao");
        }
    }
}
