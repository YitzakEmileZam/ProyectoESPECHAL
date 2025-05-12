using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pg1.Models;

namespace Pg1.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones y configuraciones adicionales si las deseas.
            modelBuilder.Entity<DetallePedido>()
                .HasOne(p => p.Producto)
                .WithMany()
                .HasForeignKey(p => p.IdProducto);

            modelBuilder.Entity<DetallePedido>()
                .HasOne(p => p.Pedido)
                .WithMany()
                .HasForeignKey(p => p.IdPedido);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany()
                .HasForeignKey(p => p.IdCategoria);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.IdCliente);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Pedido)
                .WithMany()
                .HasForeignKey(p => p.IdPedido);
        }
    }
}