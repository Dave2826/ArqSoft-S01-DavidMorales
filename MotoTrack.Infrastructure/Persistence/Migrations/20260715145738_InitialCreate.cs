using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoTrack.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motocicletas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Marca = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Ano = table.Column<int>(type: "INTEGER", nullable: false),
                    Cilindrada = table.Column<int>(type: "INTEGER", nullable: false),
                    KilometrajeActual = table.Column<int>(type: "INTEGER", nullable: false),
                    KilometrajeCompra = table.Column<int>(type: "INTEGER", nullable: true),
                    FotoUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Placas = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    VIN = table.Column<string>(type: "TEXT", maxLength: 17, nullable: true),
                    NumeroMotor = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Activa = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motocicletas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motocicletas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionesMantenimiento",
                columns: table => new
                {
                    MotocicletaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CambioAceiteKm = table.Column<int>(type: "INTEGER", nullable: false),
                    RevisionCadenaKm = table.Column<int>(type: "INTEGER", nullable: false),
                    RevisionBalatasKm = table.Column<int>(type: "INTEGER", nullable: false),
                    RevisionLlantasKm = table.Column<int>(type: "INTEGER", nullable: false),
                    RevisionFiltroAireKm = table.Column<int>(type: "INTEGER", nullable: false),
                    AjusteValvulasKm = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionesMantenimiento", x => x.MotocicletaId);
                    table.ForeignKey(
                        name: "FK_ConfiguracionesMantenimiento_Motocicletas_MotocicletaId",
                        column: x => x.MotocicletaId,
                        principalTable: "Motocicletas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gastos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MotocicletaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gastos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gastos_Motocicletas_MotocicletaId",
                        column: x => x.MotocicletaId,
                        principalTable: "Motocicletas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LecturasKilometraje",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MotocicletaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Kilometraje = table.Column<int>(type: "INTEGER", nullable: false),
                    Observaciones = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturasKilometraje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LecturasKilometraje_Motocicletas_MotocicletaId",
                        column: x => x.MotocicletaId,
                        principalTable: "Motocicletas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimientos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MotocicletaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    KilometrajeServicio = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Taller = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Motocicletas_MotocicletaId",
                        column: x => x.MotocicletaId,
                        principalTable: "Motocicletas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_MotocicletaId",
                table: "Gastos",
                column: "MotocicletaId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturasKilometraje_MotocicletaId",
                table: "LecturasKilometraje",
                column: "MotocicletaId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturasKilometraje_MotocicletaId_Fecha",
                table: "LecturasKilometraje",
                columns: new[] { "MotocicletaId", "Fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_Fecha",
                table: "Mantenimientos",
                column: "Fecha");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_MotocicletaId",
                table: "Mantenimientos",
                column: "MotocicletaId");

            migrationBuilder.CreateIndex(
                name: "IX_Motocicletas_UsuarioId",
                table: "Motocicletas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracionesMantenimiento");

            migrationBuilder.DropTable(
                name: "Gastos");

            migrationBuilder.DropTable(
                name: "LecturasKilometraje");

            migrationBuilder.DropTable(
                name: "Mantenimientos");

            migrationBuilder.DropTable(
                name: "Motocicletas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
