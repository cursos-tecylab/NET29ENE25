using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Usuario.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_rol = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    descripcion = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_persona = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    apellido_paterno = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    apellido_materno = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    nombre_usuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    fecha_nacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    correo_electronico = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    direccion_pais = table.Column<string>(type: "text", nullable: true),
                    direccion_departamento = table.Column<string>(type: "text", nullable: true),
                    direccion_provincia = table.Column<string>(type: "text", nullable: true),
                    direccion_distrito = table.Column<string>(type: "text", nullable: true),
                    direccion_calle = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false),
                    fecha_ultimo_cambio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rol_id = table.Column<Guid>(type: "uuid", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuarios_roles_rol_id",
                        column: x => x.rol_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "doble_factor_autenticacion",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false),
                    codigo = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_doble_factor_autenticacion", x => x.id);
                    table.ForeignKey(
                        name: "fk_doble_factor_autenticacion_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_doble_factor_autenticacion_usuario_id",
                table: "doble_factor_autenticacion",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_nombre_rol",
                table: "roles",
                column: "nombre_rol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_rol_id",
                table: "usuarios",
                column: "rol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "doble_factor_autenticacion");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
