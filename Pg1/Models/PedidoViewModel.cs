using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pg1.Models
{
    public class PedidoViewModel
    {
        public Carrito Carrito { get; set; }
        public string NombreCliente { get; set; }
        public string DireccionEnvio { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}