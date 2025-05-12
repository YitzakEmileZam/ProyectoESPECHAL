using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //Necesario para el uso de [Key]
using System.ComponentModel.DataAnnotations.Schema; //Necesario para el uso de [DatabaseGenerated]

namespace Pg1.Models
{
    public class DetallePedido
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int IdDetalle { get; set; }

            public int IdPedido { get; set; }
            public Pedido Pedido { get; set; }

            public int IdProducto { get; set; }
            public Producto Producto { get; set; }

            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Subtotal { get; set; }
    }
}