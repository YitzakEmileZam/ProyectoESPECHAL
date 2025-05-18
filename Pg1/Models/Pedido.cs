using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //Necesario para el uso de [Key]
using System.ComponentModel.DataAnnotations.Schema; //Necesario para el uso de [DatabaseGenerated]

namespace Pg1.Models
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }

        public ICollection<DetallePedido> Detalles { get; set; }
        public ICollection<Pago> Pagos { get; set; }
            
        public enum EstadoPedido
        {
        Carrito, Pendiente, Completado, Cancelado
        }
    }
}