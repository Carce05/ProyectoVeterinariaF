using Microsoft.EntityFrameworkCore;

namespace ProyectoVeterinaria.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options) { }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<Facturacion> Facturas { get; set; }

        public DbSet<FacturaDetalle> FacturaDetalle { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Citas> Citas { get; set; }
        public DbSet<CorreoElectronico> CorreoElectronico { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>().ToTable("Empleados");
            modelBuilder.Entity<Puesto>().ToTable("Puesto");
            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Proveedor>().ToTable("Proveedor");
            modelBuilder.Entity<Clientes>().ToTable("Clientes");
            modelBuilder.Entity<Citas>().ToTable("Citas");
            modelBuilder.Entity<CorreoElectronico>().ToTable("CorreoElectronico");
        }
    }
}
