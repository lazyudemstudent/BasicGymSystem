using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaGimnasio.Data.DbContext.Entities;

namespace SistemaGimnasio.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Entrenador> Entrenadores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<TipoMembresia> TiposMembresia { get; set; }
        public DbSet<Membresia> Membresias { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entrenador>(entity =>
            {
                entity.ToTable("Tbl_Entrenadores");
                entity.HasKey(e => e.EntrenadorId);
                entity.Property(e => e.Cedula).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Nombres).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellidos).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Especialidad).IsRequired().HasMaxLength(120);
                entity.Property(e => e.Telefono).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(150);
                entity.HasIndex(e => e.Cedula).IsUnique();
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Tbl_Clientes");
                entity.HasKey(c => c.ClienteId);
                entity.Property(c => c.Cedula).IsRequired().HasMaxLength(20);
                entity.Property(c => c.Nombres).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Apellidos).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Telefono).IsRequired().HasMaxLength(20);
                entity.Property(c => c.Correo).IsRequired().HasMaxLength(150);
                entity.HasIndex(c => c.Cedula).IsUnique();

                entity.HasOne(c => c.Entrenador)
                      .WithMany(e => e.Clientes)
                      .HasForeignKey(c => c.EntrenadorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TipoMembresia>(entity =>
            {
                entity.ToTable("Tbl_TiposMembresia");
                entity.HasKey(t => t.TipoMembresiaId);
                entity.Property(t => t.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(t => t.Descripcion).IsRequired().HasMaxLength(500);
                entity.Property(t => t.Precio).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Membresia>(entity =>
            {
                entity.ToTable("Tbl_Membresias");
                entity.HasKey(m => m.MembresiaId);
                entity.Property(m => m.PrecioAcordado).HasColumnType("decimal(18,2)");

                entity.HasOne(m => m.Cliente)
                      .WithMany(c => c.Membresias)
                      .HasForeignKey(m => m.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.TipoMembresia)
                      .WithMany(t => t.Membresias)
                      .HasForeignKey(m => m.TipoMembresiaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.ToTable("Tbl_Pagos");
                entity.HasKey(p => p.PagoId);
                entity.Property(p => p.Monto).HasColumnType("decimal(18,2)");
                entity.Property(p => p.MetodoPago).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Referencia).HasMaxLength(100);
                entity.Property(p => p.Observacion).HasMaxLength(500);

                entity.HasOne(p => p.Membresia)
                      .WithMany(m => m.Pagos)
                      .HasForeignKey(p => p.MembresiaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.ToTable("Tbl_Asistencias");
                entity.HasKey(a => a.AsistenciaId);
                entity.Property(a => a.Observacion).HasMaxLength(500);

                entity.HasOne(a => a.Cliente)
                      .WithMany(c => c.Asistencias)
                      .HasForeignKey(a => a.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
