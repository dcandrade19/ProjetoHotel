using Data.Reservas.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Reservas.Model
{
    public partial class ReservasModelDb : DbContext
    {
        public ReservasModelDb()
            : base("name=ReservasModelDb")
        {
        }

        public virtual DbSet<Bairros> Bairros { get; set; }
        public virtual DbSet<Cidades> Cidades { get; set; }
        public virtual DbSet<Hotel> Hoteis { get; set; }
        public virtual DbSet<Logradouros> Logradouros { get; set; }
        public virtual DbSet<Quarto> Quartos { get; set; }
        public virtual DbSet<Reserva> Reservas { get; set; }
        public virtual DbSet<Turista> Turistas { get; set; }
        public virtual DbSet<Ufs> Ufs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bairros>()
                .Property(e => e.DescBairro)
                .IsUnicode(false);

            modelBuilder.Entity<Bairros>()
                .HasMany(e => e.Logradouros)
                .WithRequired(e => e.Bairros)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cidades>()
                .Property(e => e.DescCidade)
                .IsUnicode(false);

            modelBuilder.Entity<Cidades>()
                .Property(e => e.FlgEstado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Cidades>()
                .HasMany(e => e.Bairros)
                .WithRequired(e => e.Cidades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Cidade)
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.Uf)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.Quartos)
                .WithRequired(e => e.Hotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Logradouros>()
                .Property(e => e.DescLogradouro)
                .IsUnicode(false);

            modelBuilder.Entity<Logradouros>()
                .Property(e => e.DescTipo)
                .IsUnicode(false);

            modelBuilder.Entity<Quarto>()
                .HasMany(e => e.Reservas)
                .WithRequired(e => e.Quarto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Turista>()
                .Property(e => e.Senha)
                .IsFixedLength();

            modelBuilder.Entity<Ufs>()
                .Property(e => e.UfId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Ufs>()
                .Property(e => e.DescUf)
                .IsUnicode(false);

            modelBuilder.Entity<Ufs>()
                .HasMany(e => e.Cidades)
                .WithRequired(e => e.Ufs)
                .HasForeignKey(e => e.FlgEstado)
                .WillCascadeOnDelete(false);
        }
    }
}
