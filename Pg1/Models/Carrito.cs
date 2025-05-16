using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pg1.Models
{
    public class Carrito
    {
        public List<CarritoItem> Items { get; set; } = new List<CarritoItem>();
        public decimal Total => Items.Sum(i => i.Subtotal);
    }

    public class CarritoItem
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}