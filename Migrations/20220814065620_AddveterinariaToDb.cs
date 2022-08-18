using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoVeterinaria.Migrations
{
    public partial class AddveterinariaToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<int>(type: "int", nullable: false),
                    NombreDueño = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NombreMascota = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RazaMascota = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TipoMascota = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Celular = table.Column<int>(type: "int", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    idProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cedula = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.idProveedor);
                });

            migrationBuilder.CreateTable(
                name: "Puesto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    puesto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puesto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CorreoElectronico",
                columns: table => new
                {
                    IdCorreo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    Destinatario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Asunto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cuerpo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorreoElectronico", x => x.IdCorreo);
                    table.ForeignKey(
                        name: "FK_CorreoElectronico_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    IdFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteIdCliente = table.Column<int>(type: "int", nullable: false),
                    NombreDueño = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NombreEmpleado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Iva = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.IdFactura);
                    table.ForeignKey(
                        name: "FK_Facturas_Clientes_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigo = table.Column<int>(type: "int", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    marca = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    precioCompra = table.Column<decimal>(type: "decimal(18,2)", maxLength: 50, nullable: false),
                    precioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    unidadMedida = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    cantidad = table.Column<double>(type: "float", maxLength: 20, nullable: false),
                    idProveedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.idProducto);
                    table.ForeignKey(
                        name: "FK_Producto_Proveedor_idProveedor",
                        column: x => x.idProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "idProveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cedula = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ApellIdempleadoos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    contra = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PuestoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleados_Puesto_PuestoID",
                        column: x => x.PuestoID,
                        principalTable: "Puesto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    IdCita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteIdCliente = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    NombreDueño = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NombreVeterinario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RazaMascota = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EstadoMascota = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FechaCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraCita = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotivoCita = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NombreServicio = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.IdCita);
                    table.ForeignKey(
                        name: "FK_Citas_Clientes_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Citas_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citas_ClienteIdCliente",
                table: "Citas",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_EmpleadoId",
                table: "Citas",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CorreoElectronico_IdCliente",
                table: "CorreoElectronico",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_PuestoID",
                table: "Empleados",
                column: "PuestoID");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ClienteIdCliente",
                table: "Facturas",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_idProveedor",
                table: "Producto",
                column: "idProveedor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "CorreoElectronico");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "Puesto");
        }
    }
}
