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

            
             // Configuración de Categoría-Producto
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.IdCategoria)
                .HasConstraintName("FK_Producto_Categoria")
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Cliente-Pedido
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.IdCliente)
                .HasConstraintName("FK_Pedido_Cliente")
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Pedido-DetallePedido
            modelBuilder.Entity<DetallePedido>()
                .HasOne(d => d.Pedido)
                .WithMany(p => p.Detalles)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK_DetallePedido_Pedido")
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de DetallePedido-Producto
            modelBuilder.Entity<DetallePedido>()
                .HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_DetallePedido_Producto")
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Pedido-Pago
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Pedido)
                .WithMany(p => p.Pagos)
                .HasForeignKey(p => p.IdPedido)
                .HasConstraintName("FK_Pago_Pedido")
                .OnDelete(DeleteBehavior.Cascade);
            
            /*modelBuilder.Entity<Pedido>().Ignore(p => p.ClienteIdCliente);
            modelBuilder.Entity<DetallePedido>().Ignore(d => d.PedidoIdPedido);
            modelBuilder.Entity<Pago>().Ignore(p => p.PedidoIdPedido);
            modelBuilder.Entity<Producto>().Ignore(p => p.CategoriaIdCategoria);*/

            modelBuilder.Entity<DetallePedido>().ToTable("DetallesPedido");
            modelBuilder.Entity<Pago>().ToTable("Pagos");     

            modelBuilder.Entity<Producto>()
                .Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Categoria>()
                .Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Pedido>()
                .Property(p => p.Estado)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Pedido>()
                .Property(p => p.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DetallePedido>()
                .Property(d => d.PrecioUnitario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DetallePedido>()
                .Property(d => d.Subtotal)
                .HasColumnType("decimal(18,2)");
            
            modelBuilder.Entity<Pedido>()
                .Property(p => p.FechaPedido)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Pedido>()
                .Property(p => p.Estado)
                .HasDefaultValue("Pendiente");
        }
    }
}