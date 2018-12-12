using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICore20.Models
{
    //TODO. 33. para agregar las tablas de identity, se debe heredar de IdentityDbContext
    //public class AppDbContext : DbContext
    public class AppDbContext : IdentityDbContext<AppUser>
    {  // TODO 1 Agregar DbContext

        public DbSet<Pais> Paises  { get; set; }

        // TODO. 16 Crear DbSet para Estados.
        public DbSet<Estado> Estados { get; set; }

        // TODO 6.  Agregar las opciones del context a la clase base.
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
    }

 
}
