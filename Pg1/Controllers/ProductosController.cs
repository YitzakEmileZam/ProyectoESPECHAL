using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore; 
using Pg1.Data;
using Pg1.Models;

namespace Pg1.Controllers
{
    /*[Route("[controller]")]*/
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly ILogger<ProductosController> _logger;

        public ProductosController(ApplicationDbContext context) //ILogger<ProductosController> logger)
        {
            _context= context;
            //_logger = logger;
        }

        /*public IActionResult Index()
        {
            return View();
        }*/

        public async Task<IActionResult> Index(string categoria, string busqueda)
        {
            IQueryable<Producto> productosQuery = _context.Productos.Include(p => p.Categoria);

            if (!string.IsNullOrEmpty(categoria))
            {
                productosQuery = productosQuery.Where(p => p.Categoria.Nombre == categoria);
            }

            if (!string.IsNullOrEmpty(busqueda))
            {
                productosQuery = productosQuery.Where(p => 
                    p.Nombre.Contains(busqueda) || 
                    p.Descripcion.Contains(busqueda));
            }

            var model = await productosQuery.ToListAsync();
            ViewBag.Categorias = await _context.Categorias.ToListAsync();
            
            return View(model);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
                
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}