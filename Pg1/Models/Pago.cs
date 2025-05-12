using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //Necesario para el uso de [Key]
using System.ComponentModel.DataAnnotations.Schema; //Necesario para el uso de [DatabaseGenerated]

namespace Pg1.Models
{
    public class Pago
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int IdPago { get; set; }

            public int IdPedido { get; set; }
            public Pedido Pedido { get; set; }

            public DateTime FechaPago { get; set; }
            public decimal Monto { get; set; }
            public string MetodoPago { get; set; }
            public string EstadoPago { get; set; }
    }
}