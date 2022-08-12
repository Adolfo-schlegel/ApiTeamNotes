using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TeamsNotesApi.Models.Models_CVM_GPA_SEG_01;

namespace TeamsNotesApi.Services.Connections
{
    public partial class CVM_GPA_SEG_01Context : DbContext
    {
        public CVM_GPA_SEG_01Context()
        {
        }

        public CVM_GPA_SEG_01Context(DbContextOptions<CVM_GPA_SEG_01Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=CVM_GPA_SEG_01;User ID=SA;Password=10deagosto; trustServerCertificate=true; Trusted_Connection=False; MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__USUARIOS__91136B9107020F21")
                    .IsClustered(false);

                entity.ToTable("USUARIOS");

                entity.Property(e => e.IdUsuario)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_USUARIO");

                entity.Property(e => e.CdUsuario)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CD_USUARIO");

                entity.Property(e => e.DhAlta)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("DH_ALTA");

                entity.Property(e => e.DhModifica)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("DH_MODIFICA");

                entity.Property(e => e.DsMail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DS_MAIL");

                entity.Property(e => e.DsPassword)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("DS_PASSWORD");

                entity.Property(e => e.DsUsuario)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DS_USUARIO");

                entity.Property(e => e.DtAcceso)
                    .HasColumnType("date")
                    .HasColumnName("DT_ACCESO");

                entity.Property(e => e.DtAlta)
                    .HasColumnType("date")
                    .HasColumnName("DT_ALTA");

                entity.Property(e => e.DtLock)
                    .HasColumnType("date")
                    .HasColumnName("DT_LOCK");

                entity.Property(e => e.DtModifica)
                    .HasColumnType("date")
                    .HasColumnName("DT_MODIFICA");

                entity.Property(e => e.DtPassword)
                    .HasColumnType("date")
                    .HasColumnName("DT_PASSWORD");

                entity.Property(e => e.IdUsuarioAlta).HasColumnName("ID_USUARIO_ALTA");

                entity.Property(e => e.IdUsuarioModifica).HasColumnName("ID_USUARIO_MODIFICA");

                entity.Property(e => e.LgAnulado).HasColumnName("LG_ANULADO");

                entity.Property(e => e.LgCambiarPassword).HasColumnName("LG_CAMBIAR_PASSWORD");

                entity.Property(e => e.LgLock).HasColumnName("LG_LOCK");

                entity.Property(e => e.QtIntentos).HasColumnName("QT_INTENTOS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
