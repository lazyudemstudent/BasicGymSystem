using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using SistemaGimnasio.Data.DbContext;
using SistemaGimnasio.Data.DbContext.Entities;

#nullable disable

namespace SistemaGimnasio.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    public class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity<IdentityRole>(b =>
            {
                b.Property<string>("Id").HasColumnType("nvarchar(450)");
                b.Property<string>("ConcurrencyStamp").IsConcurrencyToken().HasColumnType("nvarchar(max)");
                b.Property<string>("Name").HasMaxLength(256).HasColumnType("nvarchar(256)");
                b.Property<string>("NormalizedName").HasMaxLength(256).HasColumnType("nvarchar(256)");
                b.HasKey("Id");
                b.HasIndex("NormalizedName").IsUnique().HasDatabaseName("RoleNameIndex").HasFilter("[NormalizedName] IS NOT NULL");
                b.ToTable("AspNetRoles", (string)null);
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int").UseIdentityColumn();
                b.Property<string>("ClaimType").HasColumnType("nvarchar(max)");
                b.Property<string>("ClaimValue").HasColumnType("nvarchar(max)");
                b.Property<string>("RoleId").IsRequired().HasColumnType("nvarchar(450)");
                b.HasKey("Id");
                b.HasIndex("RoleId");
                b.ToTable("AspNetRoleClaims", (string)null);
            });

            modelBuilder.Entity<IdentityUser>(b =>
            {
                b.Property<string>("Id").HasColumnType("nvarchar(450)");
                b.Property<int>("AccessFailedCount").HasColumnType("int");
                b.Property<string>("ConcurrencyStamp").IsConcurrencyToken().HasColumnType("nvarchar(max)");
                b.Property<string>("Email").HasMaxLength(256).HasColumnType("nvarchar(256)");
                b.Property<bool>("EmailConfirmed").HasColumnType("bit");
                b.Property<bool>("LockoutEnabled").HasColumnType("bit");
                b.Property<DateTimeOffset?>("LockoutEnd").HasColumnType("datetimeoffset");
                b.Property<string>("NormalizedEmail").HasMaxLength(256).HasColumnType("nvarchar(256)");
                b.Property<string>("NormalizedUserName").HasMaxLength(256).HasColumnType("nvarchar(256)");
                b.Property<string>("PasswordHash").HasColumnType("nvarchar(max)");
                b.Property<string>("PhoneNumber").HasColumnType("nvarchar(max)");
                b.Property<bool>("PhoneNumberConfirmed").HasColumnType("bit");
                b.Property<string>("SecurityStamp").HasColumnType("nvarchar(max)");
                b.Property<bool>("TwoFactorEnabled").HasColumnType("bit");
                b.Property<string>("UserName").HasMaxLength(256).HasColumnType("nvarchar(256)");
                b.HasKey("Id");
                b.HasIndex("NormalizedEmail").HasDatabaseName("EmailIndex");
                b.HasIndex("NormalizedUserName").IsUnique().HasDatabaseName("UserNameIndex").HasFilter("[NormalizedUserName] IS NOT NULL");
                b.ToTable("AspNetUsers", (string)null);
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int").UseIdentityColumn();
                b.Property<string>("ClaimType").HasColumnType("nvarchar(max)");
                b.Property<string>("ClaimValue").HasColumnType("nvarchar(max)");
                b.Property<string>("UserId").IsRequired().HasColumnType("nvarchar(450)");
                b.HasKey("Id");
                b.HasIndex("UserId");
                b.ToTable("AspNetUserClaims", (string)null);
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.Property<string>("LoginProvider").HasMaxLength(128).HasColumnType("nvarchar(128)");
                b.Property<string>("ProviderKey").HasMaxLength(128).HasColumnType("nvarchar(128)");
                b.Property<string>("ProviderDisplayName").HasColumnType("nvarchar(max)");
                b.Property<string>("UserId").IsRequired().HasColumnType("nvarchar(450)");
                b.HasKey("LoginProvider", "ProviderKey");
                b.HasIndex("UserId");
                b.ToTable("AspNetUserLogins", (string)null);
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.Property<string>("UserId").HasColumnType("nvarchar(450)");
                b.Property<string>("RoleId").HasColumnType("nvarchar(450)");
                b.HasKey("UserId", "RoleId");
                b.HasIndex("RoleId");
                b.ToTable("AspNetUserRoles", (string)null);
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.Property<string>("UserId").HasColumnType("nvarchar(450)");
                b.Property<string>("LoginProvider").HasMaxLength(128).HasColumnType("nvarchar(128)");
                b.Property<string>("Name").HasMaxLength(128).HasColumnType("nvarchar(128)");
                b.Property<string>("Value").HasColumnType("nvarchar(max)");
                b.HasKey("UserId", "LoginProvider", "Name");
                b.ToTable("AspNetUserTokens", (string)null);
            });

            modelBuilder.Entity<Entrenador>(b =>
            {
                b.Property<int>("EntrenadorId").ValueGeneratedOnAdd().HasColumnType("int").UseIdentityColumn();
                b.Property<bool>("Activo").HasColumnType("bit");
                b.Property<string>("Apellidos").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
                b.Property<string>("Cedula").IsRequired().HasMaxLength(20).HasColumnType("nvarchar(20)");
                b.Property<string>("Correo").IsRequired().HasMaxLength(150).HasColumnType("nvarchar(150)");
                b.Property<string>("Especialidad").IsRequired().HasMaxLength(120).HasColumnType("nvarchar(120)");
                b.Property<string>("Nombres").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
                b.Property<string>("Telefono").IsRequired().HasMaxLength(20).HasColumnType("nvarchar(20)");
                b.HasKey("EntrenadorId");
                b.HasIndex("Cedula").IsUnique();
                b.ToTable("Tbl_Entrenadores", (string)null);
            });

            modelBuilder.Entity<Cliente>(b =>
            {
                b.Property<int>("ClienteId").ValueGeneratedOnAdd().HasColumnType("int").UseIdentityColumn();
                b.Property<bool>("Activo").HasColumnType("bit");
                b.Property<string>("Apellidos").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
                b.Property<string>("Cedula").IsRequired().HasMaxLength(20).HasColumnType("nvarchar(20)");
                b.Property<string>("Correo").IsRequired().HasMaxLength(150).HasColumnType("nvarchar(150)");
                b.Property<int?>("EntrenadorId").HasColumnType("int");
                b.Property<DateTime>("FechaNacimiento").HasColumnType("datetime2");
                b.Property<DateTime>("FechaRegistro").HasColumnType("datetime2");
                b.Property<string>("Nombres").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
                b.Property<string>("Telefono").IsRequired().HasMaxLength(20).HasColumnType("nvarchar(20)");
                b.HasKey("ClienteId");
                b.HasIndex("Cedula").IsUnique();
                b.HasIndex("EntrenadorId");
                b.ToTable("Tbl_Clientes", (string)null);
            });

            modelBuilder.Entity<TipoMembresia>(b =>
            {
                b.Property<int>("TipoMembresiaId").ValueGeneratedOnAdd().HasColumnType("int").UseIdentityColumn();
                b.Property<bool>("Activa").HasColumnType("bit");
                b.Property<string>("Descripcion").IsRequired().HasMaxLength(500).HasColumnType("nvarchar(500)");
                b.Property<int>("DuracionDias").HasColumnType("int");
                b.Property<string>("Nombre").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
                b.Property<decimal>("Precio").HasColumnType("decimal(18,2)");
                b.HasKey("TipoMembresiaId");
                b.ToTable("Tbl_TiposMembresia", (string)null);
            });

            modelBuilder.Entity<Membresia>(b =>
            {
                b.Property<int>("MembresiaId").ValueGeneratedOnAdd().HasColumnType("int").UseIdentityColumn();
                b.Property<bool>("Activa").HasColumnType("bit");
                b.Property<int>("ClienteId").HasColumnType("int");
                b.Property<DateTime>("FechaFin").HasColumnType("datetime2");
                b.Property<DateTime>("FechaInicio").HasColumnType("datetime2");
                b.Property<decimal>("PrecioAcordado").HasColumnType("decimal(18,2)");
                b.Property<int>("TipoMembresiaId").HasColumnType("int");
                b.HasKey("MembresiaId");
                b.HasIndex("ClienteId");
                b.HasIndex("TipoMembresiaId");
                b.ToTable("Tbl_Membresias", (string)null);
            });

            modelBuilder.Entity<Pago>(b =>
            {
                b.Property<int>("PagoId").ValueGeneratedOnAdd().HasColumnType("int").UseIdentityColumn();
                b.Property<DateTime>("FechaPago").HasColumnType("datetime2");
                b.Property<int>("MembresiaId").HasColumnType("int");
                b.Property<string>("MetodoPago").IsRequired().HasMaxLength(50).HasColumnType("nvarchar(50)");
                b.Property<decimal>("Monto").HasColumnType("decimal(18,2)");
                b.Property<string>("Observacion").HasMaxLength(500).HasColumnType("nvarchar(500)");
                b.Property<string>("Referencia").HasMaxLength(100).HasColumnType("nvarchar(100)");
                b.HasKey("PagoId");
                b.HasIndex("MembresiaId");
                b.ToTable("Tbl_Pagos", (string)null);
            });

            modelBuilder.Entity<Asistencia>(b =>
            {
                b.Property<int>("AsistenciaId").ValueGeneratedOnAdd().HasColumnType("int").UseIdentityColumn();
                b.Property<int>("ClienteId").HasColumnType("int");
                b.Property<DateTime>("FechaHoraEntrada").HasColumnType("datetime2");
                b.Property<DateTime?>("FechaHoraSalida").HasColumnType("datetime2");
                b.Property<string>("Observacion").HasMaxLength(500).HasColumnType("nvarchar(500)");
                b.HasKey("AsistenciaId");
                b.HasIndex("ClienteId");
                b.ToTable("Tbl_Asistencias", (string)null);
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.HasOne<IdentityRole>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.Cascade).IsRequired();
            });
            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.HasOne<IdentityUser>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade).IsRequired();
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasOne<IdentityUser>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade).IsRequired();
            });
            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasOne<IdentityRole>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.Cascade).IsRequired();
                b.HasOne<IdentityUser>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade).IsRequired();
            });
            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.HasOne<IdentityUser>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade).IsRequired();
            });
            modelBuilder.Entity<Cliente>(b =>
            {
                b.HasOne(c => c.Entrenador).WithMany(e => e.Clientes).HasForeignKey(c => c.EntrenadorId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Membresia>(b =>
            {
                b.HasOne(m => m.Cliente).WithMany(c => c.Membresias).HasForeignKey(m => m.ClienteId).OnDelete(DeleteBehavior.Restrict).IsRequired();
                b.HasOne(m => m.TipoMembresia).WithMany(t => t.Membresias).HasForeignKey(m => m.TipoMembresiaId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            });
            modelBuilder.Entity<Pago>(b =>
            {
                b.HasOne(p => p.Membresia).WithMany(m => m.Pagos).HasForeignKey(p => p.MembresiaId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            });
            modelBuilder.Entity<Asistencia>(b =>
            {
                b.HasOne(a => a.Cliente).WithMany(c => c.Asistencias).HasForeignKey(a => a.ClienteId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            });
#pragma warning restore 612, 618
        }
    }
}
