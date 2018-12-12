using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPICore20.Models;

namespace WebAPICore20
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // TODO 2  Para usar base de datos en memoria.
            services.AddDbContext<AppDbContext>(options => {
                options.UseInMemoryDatabase("paises");
               });

            services.AddMvc().AddJsonOptions(options=> {
                //TODO 19. Agregar opcion de json para ignorar el error de reference looping handler.
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                }
            );
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // TODO 7. Agregar  AppDbContext al conigure
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //TODO  8. Si no hay datos, llenar con algunos de prueba.
            if (!context.Paises.Any())
            {
                context.Paises.AddRange (
                    new Pais() {
                            Nombre = "Venezuela",
                            Estados = new List<Estado>(){
                            new Estado {Nombre="Miranda"},
                            new Estado {Nombre="Dist Fereral"}   // TODO 17. Agregar estados de prueba.
                        }
                    },

                    new Pais()
                        {
                            Nombre = "Coombia",
                            Estados = new List<Estado>(){
                            new Estado {Nombre="Medellin"},
                            new Estado {Nombre="Bogota Fereral"}
                        }
                        }
                    );

                context.SaveChanges();
            }
            

            app.UseMvc();
        }
    }
}
