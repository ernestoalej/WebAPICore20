using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICore20.Models;

namespace WebAPICore20.Controllers
{
    [Produces("application/json")]  
    [Route("api/Pais")]    

    //TODO. 35 Impedir el acceso al controlador a todos aquellos usuarios que no estén autorizados.      // Se produce un 401 Unauthorized.
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]  

    public class PaisController : Controller
    { 
    
        private readonly AppDbContext context;

        // TODO 3. Crear Controlador PaisController


        // TODO 4. Crear constructor y agregar context y parámetro y campo.
        public PaisController(AppDbContext context)
        {
            this.context = context;
        }


        // TODO 5. crear metodo Get.
        [HttpGet]
        public IEnumerable<Pais> Get()
        {
            // TODO 18. Incluir estado en el Get usando linq.
            //TODO 20.   Quitar el include de Estados y colocarlo en GetById. Para no traer tantos datos cuando se cargan todos los pasies.
            //return context.Paises.Include(p => p.Estados).ToList();

            return context.Paises.ToList();

        }

        //TODO  9 . Crear metodo Get para consultar por id.
        //TODO  11 . Agregar un nombre 'GetById' a la ruta poder ser llamada desde post.

        [HttpGet("{id}", Name = "PaisCreado")]
        public IActionResult GetById(int id)
        {
            // TODO 21. Agregar include para agregar los Estados
            //var pais = context.Paises.FirstOrDefault(p => p.Id == id);
            
            var pais = context.Paises.Include(p => p.Estados).FirstOrDefault(p => p.Id == id);

            if (pais == null)
            {
                return NotFound();  // 404 Not Found.
            }

            return Ok(pais);  //  200 Ok.
        }


        //TODO  10 . Crear metodo Post para agregar un pais.
        [HttpPost]
        public IActionResult Post([FromBody] Pais pais)
        {
            if (ModelState.IsValid)
            {
                context.Paises.Add(pais);
                context.SaveChanges();

                return new CreatedAtRouteResult("PaisCreado", new { id = pais.Id }, pais);  // 201 Created.
            }

            return BadRequest(ModelState);  // 400 Bad Request.
        }

        //TODO  13 . Crear metodo Put para actualizar un pais.
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Pais pais, int id)
        {
            if (ModelState.IsValid && id == pais.Id)
            {
                context.Entry(pais).State = EntityState.Modified;
                context.SaveChanges();

                return Ok();  //200 Ok.
            }

            return BadRequest(); // 400 Bad Request.

        }

        //TODO  14 . Crear metodo Delete para eliminar un pais.
        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] Pais pais, int id)
        {
            if (ModelState.IsValid && id == pais.Id)
            {
                context.Remove(pais);
                context.SaveChanges();

                return Ok(pais);  //200 Ok.
            }

            return BadRequest(); // 400 Bad Request.

        }

    }
}