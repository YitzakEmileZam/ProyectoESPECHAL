using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //Necesario para el uso de [Key]
using System.ComponentModel.DataAnnotations.Schema; //Necesario para el uso de [DatabaseGenerated]

namespace Pg1.Models
{
    public class Cliente
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int IdCliente { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Email { get; set; }
            public string Telefono { get; set; }
            public string Direccion { get; set; }
            public DateTime FechaRegistro { get; set; }

            public ICollection<Pedido> Pedidos { get; set; }
    }
}