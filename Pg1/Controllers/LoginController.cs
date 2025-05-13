using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Pg1.Data; //Necesario para el uso de ApplicationDbContext
using Pg1.Models; //Necesario para el uso de ApplicationDbContext

namespace Pg1.Controllers
{

    public class LoginController : Controller
    {
    private readonly ApplicationDbContext _context;
    private readonly ILogger<LoginController> _logger;

    public LoginController(ApplicationDbContext context, ILogger<LoginController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Ingresar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Ingresar(string email, string password)
    {
        var cliente = _context.Clientes.FirstOrDefault(c => c.Email == email && c.Password == password);
        if (cliente != null)
        {
            HttpContext.Session.SetString("Usuario", cliente.Nombre); // O Email si prefieres
            return RedirectToAction("Index", "Home");
        }
        ViewBag.Mensaje = "Email o contraseña incorrectos.";
        return View();
    }

        public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Ingresar", "Login");
    }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}