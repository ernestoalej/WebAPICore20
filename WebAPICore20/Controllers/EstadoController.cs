using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICore20.Models;

namespace WebAPICore20.Controllers
{
    [Produces("application/json")]

    //TODO. 23 cambiar la ruta agregando pais antes de  Estado.
    [Route("api/Pais/{PaisId}/Estado")]
    public class EstadoController : Controller
    {
        private readonly AppDbContext context;

        public EstadoController(AppDbContext context)
        {
            this.context = context;
        }

        //TODO. 24 crear metodo get para devolver los estados de un pais.
        [HttpGet]
        public IActionResult Get(int PaisId)
        {
            var paisesEstados = context.Estados.Where(e => e.PaisId == PaisId).ToList();

            return Ok(paisesEstados);
        }

        //TODO. 25 crear metodo get para devolver los estados por Id.
        //TODO. 27 crear nombre a la ruta  poder llamarle desde el post, despues de crear un nuevo estado.
        [HttpGet("{EstadoId}", Name = "EstadoCreado")]
        public IActionResult GetById(int EstadoId)
        {

            var estado = context.Estados.Where(e => e.Id == EstadoId).ToList();

            if (estado == null)
            {
                return NotFound();
            }

            return Ok(estado);   // 200  Ok
        }

        //TODO. 26 crear metodo post para guardar los estados
        [HttpPost]
        public IActionResult Post([FromBody] Estado estado, int PaisId)
        {
            if (ModelState.IsValid && PaisId == estado.PaisId)
            {
                context.Estados.Add(estado);
                context.SaveChanges();

                return new CreatedAtRouteResult("EstadoCreado", new { EstadoId = estado.Id }, estado);
            }

            return BadRequest(ModelState);  // 400 Bad Request.
        }

        //TODO  27 . Crear metodo Put para actualizar un pais.
        [HttpPut("{EstadoID}")]
        public IActionResult Put([FromBody] Estado Estado, int EstadoID)
        {
            if (ModelState.IsValid && Estado.Id == EstadoID)
            {
                context.Entry(Estado).State = EntityState.Modified;
                context.SaveChanges();

                return Ok();  //200 Ok.
            }

            return BadRequest();

        }

        //TODO  28 . Crear metodo Delete para eliminar un pais.
        [HttpDelete("{EstadoID}")]
        public IActionResult Delete([FromBody] Estado Estado, int EstadoID)
        {
            if (ModelState.IsValid && Estado.Id == EstadoID)
            {
                context.Remove(Estado);
                context.SaveChanges();

                return Ok(Estado);  //200 Ok.
            }

            return BadRequest();  // 400 Bad Request.
        }


    }
}