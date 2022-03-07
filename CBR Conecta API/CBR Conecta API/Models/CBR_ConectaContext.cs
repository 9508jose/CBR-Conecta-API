using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CBR_Conecta_API.Models
{
    public partial class CBR_ConectaContext : DbContext
    {
        public CBR_ConectaContext()
        {
        }

        public CBR_ConectaContext(DbContextOptions<CBR_ConectaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cifrado> Cifrados { get; set; }
        public virtual DbSet<TbArchivo> TbArchivos { get; set; }
        public virtual DbSet<TbCatalogoDocumento> TbCatalogoDocumentos { get; set; }
        public virtual DbSet<TbObra> TbObras { get; set; }
        public virtual DbSet<TbObraAsignadum> TbObraAsignada { get; set; }
        public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=177.231.253.170;Database=CBR_Conecta;User Id=usuario;Password=admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Cifrado>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("$cifrado$");

                entity.Property(e => e.App)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Cifrado1)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("$Cifrado$");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Variable)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbArchivo>(entity =>
            {
                entity.ToTable("tb_Archivos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Extension)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Obra).HasColumnName("obra");

                entity.Property(e => e.Ubicacion)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.ObraNavigation)
                    .WithMany(p => p.TbArchivos)
                    .HasForeignKey(d => d.Obra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Archivo__obra__60A75C0F");

                entity.HasOne(d => d.RubroNavigation)
                    .WithMany(p => p.TbArchivos)
                    .HasForeignKey(d => d.Rubro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Archiv__Rubro__5EBF139D");

                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.TbArchivos)
                    .HasForeignKey(d => d.Usuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Archiv__Usuar__5FB337D6");
            });

            modelBuilder.Entity<TbCatalogoDocumento>(entity =>
            {
                entity.ToTable("tb_CatalogoDocumentos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Extension)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Identificador).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rubro)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ubicacion)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<TbObra>(entity =>
            {
                entity.ToTable("tb_Obra");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Destajo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("destajo");

                entity.Property(e => e.Documento)
                    .HasColumnType("text")
                    .HasColumnName("documento");

                entity.Property(e => e.Estatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("date")
                    .HasColumnName("fechaInicio");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<TbObraAsignadum>(entity =>
            {
                entity.ToTable("tb_obraAsignada");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdObra).HasColumnName("ID_obra");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_usuario");

                entity.HasOne(d => d.IdObraNavigation)
                    .WithMany(p => p.TbObraAsignada)
                    .HasForeignKey(d => d.IdObra)
                    .HasConstraintName("FK__tb_obraAs__ID_ob__4AB81AF0");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TbObraAsignada)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__tb_obraAs__ID_us__4BAC3F29");
            });

            modelBuilder.Entity<TbUsuario>(entity =>
            {
                entity.ToTable("tb_Usuarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Departamento)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Puesto)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
