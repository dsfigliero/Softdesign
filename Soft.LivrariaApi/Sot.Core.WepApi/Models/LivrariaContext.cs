using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Soft.Modelos;

#nullable disable

namespace Soft.Models
{
    public partial class LivrariaContext : DbContext
    {
        public LivrariaContext()
        {
        }

        public LivrariaContext(DbContextOptions<LivrariaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Livro> Livros { get; set; }
        public virtual DbSet<LivroUsuario> LivroUsuarios { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-K6VKV1C\\Principal; Database=Soft.WebApi.DAL.LivrariaContext;User ID=sa;Password=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Livro>(entity =>
            {
                entity.ToTable("Livro");
            });

            modelBuilder.Entity<LivroUsuario>(entity =>
            {
                entity.ToTable("LivroUsuario");

                entity.HasOne(d => d.Livro)
                    .WithMany(p => p.LivroUsuarios)
                    .HasForeignKey(d => d.LivroId)
                    .HasConstraintName("FK_dbo.LivroUsuario_dbo.Livro_LivroId");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.LivroUsuarios)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_dbo.LivroUsuario_dbo.Usuario_UsuarioId");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
