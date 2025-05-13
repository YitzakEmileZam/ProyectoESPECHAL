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

            [Required(ErrorMessage = "El teléfono es obligatorio.")]
            [RegularExpression(@"^\d{9}$", ErrorMessage = "El teléfono debe tener 9 dígitos.")]
            public string Telefono { get; set; }
            public string Direccion { get; set; }
            public DateTime FechaRegistro { get; set; }

            public string Password { get; set; }

            public ICollection<Pedido> Pedidos { get; set; }
    }
}