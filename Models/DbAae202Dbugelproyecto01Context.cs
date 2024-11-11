using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIproyectoUgel.Models;

public partial class DbAae202Dbugelproyecto01Context : DbContext
{
    public DbAae202Dbugelproyecto01Context()
    {
    }

    public DbAae202Dbugelproyecto01Context(DbContextOptions<DbAae202Dbugelproyecto01Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Mantenimiento> Mantenimientos { get; set; }

    public virtual DbSet<MantenimientosProgramado> MantenimientosProgramados { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=SQL5106.site4now.net;database=db_aae202_dbugelproyecto01;uid=db_aae202_dbugelproyecto01_admin;pwd=senati2024");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.IdEquipo).HasName("PK__equipos__981ACF539DE9D5DC");

            entity.ToTable("equipos");

            entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");
            entity.Property(e => e.AreaEqui)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("areaEqui");
            entity.Property(e => e.EstadoEqui)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("estadoEqui");
            entity.Property(e => e.FechaAdquisicion)
                .HasColumnType("datetime")
                .HasColumnName("fechaAdquisicion");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.MarcaEqui)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("marcaEqui");
            entity.Property(e => e.ModeloEqui)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("modeloEqui");
            entity.Property(e => e.NombreEqui)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("nombreEqui");
            entity.Property(e => e.SerieEqui)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("serieEqui");
            entity.Property(e => e.TipoEqui)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("tipoEqui");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.HasKey(e => e.IdManteni).HasName("PK__mantenim__D43E2B0390A42F18");

            entity.ToTable("mantenimientos");

            entity.Property(e => e.IdManteni).HasColumnName("idManteni");
            entity.Property(e => e.CostoManteni).HasColumnName("costoManteni");
            entity.Property(e => e.DescriManteni)
                .HasColumnType("text")
                .HasColumnName("descriManteni");
            entity.Property(e => e.FechaManteni)
                .HasColumnType("datetime")
                .HasColumnName("fechaManteni");
            entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.TipoManteni)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipoManteni");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.IdEquipo)
                .HasConstraintName("fk_equi");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<MantenimientosProgramado>(entity =>
        {
            entity.HasKey(e => e.IdManPro).HasName("PK__mantenim__1B1E24EEF7026353");

            entity.ToTable("mantenimientosProgramados");

            entity.Property(e => e.IdManPro).HasColumnName("idManPro");
            entity.Property(e => e.DescriManPro)
                .HasColumnType("text")
                .HasColumnName("descriManPro");
            entity.Property(e => e.EstadoManPro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente")
                .HasColumnName("estadoManPro");
            entity.Property(e => e.FechaManPro)
                .HasColumnType("datetime")
                .HasColumnName("fechaManPro");
            entity.Property(e => e.FechaRealizado)
                .HasColumnType("datetime")
                .HasColumnName("fechaRealizado");
            entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");
            entity.Property(e => e.TipoManPro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipoManPro");

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.MantenimientosProgramados)
                .HasForeignKey(d => d.IdEquipo)
                .HasConstraintName("fk_equipo");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__roles__3C872F768DEBD81C");

            entity.ToTable("roles");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.DescriRol)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descriRol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreRol");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__usuarios__3717C9828DF1A5F6");

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.ContraUser)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("contraUser");
            entity.Property(e => e.EstadoUser).HasColumnName("estadoUser");
            entity.Property(e => e.FechaCreacionUser)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacionUser");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.NombreUser)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreUser");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("fk_rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
