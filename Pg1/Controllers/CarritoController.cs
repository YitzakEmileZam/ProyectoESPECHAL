using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pg1.Data;
using Pg1.Models;
using Pg1.Extensions;

namespace Pg1.Controllers
{
    /*[Route("[controller]")]*/
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string SessionKeyName = "_Carrito";
        private readonly ILogger<CarritoController> _logger;

        public CarritoController(ApplicationDbContext context, ILogger<CarritoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var carrito = GetCarrito();
            return View(carrito);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        

        [HttpPost]
        public async Task<IActionResult> AgregarAlCarrito(int idProducto, int cantidad = 1)
        {
            var producto = await _context.Productos.FindAsync(idProducto);

            if (producto == null || producto.Stock < cantidad)
            {
                return NotFound();
            }

            var carrito = GetCarrito();
            var item = carrito.Items.FirstOrDefault(i => i.Producto.IdProducto == idProducto);

            if (item != null)
            {
                item.Cantidad += cantidad;
            }
            else
            {
                carrito.Items.Add(new CarritoItem
                {
                    Producto = producto,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio
                });
            }

            SaveCarrito(carrito);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult ActualizarCarrito(int idProducto, int cantidad)
        {
            var carrito = GetCarrito();
            var item = carrito.Items.FirstOrDefault(i => i.Producto.IdProducto == idProducto);

            if (item != null)
            {
                item.Cantidad = cantidad;
                SaveCarrito(carrito);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult RemoverDelCarrito(int idProducto)
        {
            var carrito = GetCarrito();
            var item = carrito.Items.FirstOrDefault(i => i.Producto.IdProducto == idProducto);

            if (item != null)
            {
                carrito.Items.Remove(item);
                SaveCarrito(carrito);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var carrito = GetCarrito();
            if (!carrito.Items.Any())
            {
                return RedirectToAction("Index");
            }

            return View(new PedidoViewModel
            {
                Carrito = carrito
            });
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(PedidoViewModel model)
        {
            var carrito = GetCarrito();
            if (!carrito.Items.Any())
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var pedido = new Pedido
                {
                    FechaPedido = DateTime.Now,
                    Estado = "Pendiente",
                    Total = carrito.Items.Sum(i => i.Subtotal),
                    // Aquí deberías asignar el cliente real (usuario autenticado)
                    IdCliente = 1, // Temporal - reemplazar con autenticación
                    Detalles = carrito.Items.Select(i => new DetallePedido
                    {
                        IdProducto = i.Producto.IdProducto,
                        Cantidad = i.Cantidad,
                        PrecioUnitario = i.PrecioUnitario,
                        Subtotal = i.Subtotal
                    }).ToList()
                };

                _context.Pedidos.Add(pedido);
                
                // Actualizar stock
                foreach (var item in carrito.Items)
                {
                    var producto = await _context.Productos.FindAsync(item.Producto.IdProducto);
                    if (producto != null)
                    {
                        producto.Stock -= item.Cantidad;
                    }
                }

                await _context.SaveChangesAsync();
                HttpContext.Session.Remove(SessionKeyName);

                return RedirectToAction("Confirmacion", new { idPedido = pedido.IdPedido });
            }

            model.Carrito = carrito;
            return View(model);
        }

        public IActionResult Confirmacion(int idPedido)
        {
            var pedido = _context.Pedidos
                .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
                .FirstOrDefault(p => p.IdPedido == idPedido);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        private Carrito GetCarrito()
        {
            var carrito = HttpContext.Session.Get<Carrito>(SessionKeyName);
            if (carrito == null)
            {
                carrito = new Carrito();
                SaveCarrito(carrito);
            }
            return carrito;
        }

        private void SaveCarrito(Carrito carrito)
        {
            HttpContext.Session.Set(SessionKeyName, carrito);
        }
    }
}