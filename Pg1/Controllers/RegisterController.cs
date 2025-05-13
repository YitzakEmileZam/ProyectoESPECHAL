using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pg1.Data;
using Pg1.Models;
using Microsoft.EntityFrameworkCore;

namespace Pg1.Controllers
{

    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ApplicationDbContext context, ILogger<RegisterController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult Registrar()
        {
            return View(new Cliente());
        }

        [HttpPost]
        public IActionResult Registrar(Cliente cliente)
        {
            if (_context.Clientes.Any(c => c.Email == cliente.Email))
            {
                ViewBag.Mensaje = "El correo ya está registrado.";
                return View(cliente); // <-- Pasa el modelo aquí
            }

            cliente.FechaRegistro = DateTime.UtcNow;
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            ViewBag.Mensaje = "¡Registro exitoso!";
            ModelState.Clear(); // Limpia el formulario
            return View(new Cliente()); // <-- Devuelve un modelo vacío tras éxito
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}