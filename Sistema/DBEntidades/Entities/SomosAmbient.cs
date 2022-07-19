using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DBEntidades.Entities
{
    public partial class SomosAmbient : DbContext
    {
        public SomosAmbient()
            : base("name=SomosAmbient")
        {
        }

        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<Perfiles> Perfiles { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estados>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Estados>()
                .Property(e => e.Entidad)
                .IsUnicode(false);

            modelBuilder.Entity<Perfiles>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.User)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
