using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore20.Models
{
    public class Pais
    {
        public int Id { get; set; }

        public Pais()
        {
            //TODO. 22 Agregar nueva instancia de listado de Estados para que no se mustren null en el json.
            Estados = new List<Estado>();
        }


        // TODO  12 . Agregar una DataAnnotations para probar el 400 BadRequest al hacer un post de Pais.
        [StringLength(20, MinimumLength = 3 , ErrorMessage = "El nombre del pais debe tener entre {2} y {1} caracteres")]
        public string Nombre { get; set; }

        public List<Estado> Estados { get; set; }
    }
}
