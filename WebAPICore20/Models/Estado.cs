using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore20.Models
{
    public class Estado
    {  // TODO. 15 Crear modelo Estado, agregarle Clave Foranea.

        public int Id { get; set; }
        public string Nombre  { get; set; }
        [ForeignKey("Pais")]
        public int PaisId { get; set; }

        //TODO. 25 Agregar JsonIgnore para evitar que aparezca pais null.

        [JsonIgnore]
        public Pais Pais { get; set; }

      
    }
}
