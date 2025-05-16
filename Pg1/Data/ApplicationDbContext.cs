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

            // Relaciones y configuraciones
            //Producto -> Categoria
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.IdCategoria)
                .OnDelete(DeleteBehavior.Restrict);//con esto no se elimina en cascada
            

            //Pedido -> Cliente
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);
            
            //Pedido -> DetallePedido 
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Detalles)
                .WithOne(d => d.Pedido)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.Cascade);


            //DetallePedido -> Producto
            modelBuilder.Entity<DetallePedido>()
                .HasOne(p => p.Producto)
                .WithMany()
                .HasForeignKey(p => p.IdProducto);

            //Pedido -> Pago
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Pagos)
                .WithOne()
                .HasForeignKey(p => p.IdPedido)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetallePedido>().ToTable("DetallesPedido");
            modelBuilder.Entity<Pago>().ToTable("Pagos");


            /*modelBuilder.Entity<DetallePedido>()
                .HasOne(p => p.Pedido)
                .WithMany()
                .HasForeignKey(p => p.IdPedido);
                                    

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Pedido)
                .WithMany()
                .HasForeignKey(p => p.IdPedido);*/

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