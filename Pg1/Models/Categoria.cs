using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //Necesario para el uso de [Key]
using System.ComponentModel.DataAnnotations.Schema; //Necesario para el uso de [DatabaseGenerated]

namespace Pg1.Models
{
    public class Categoria
    {
                [Key]
                [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
                public int IdCategoria { get; set; }
                public string Nombre { get; set; }
                public string Descripcion { get; set; }
                public ICollection<Producto> Productos { get; set; }
    }
}