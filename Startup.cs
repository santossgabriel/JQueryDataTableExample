using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Services;

namespace JQueryDataTableExample
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      // PokemonRepository repository = new PokemonRepository();
      // PokemonService service = new PokemonService();
      // var pokemons = service.GetAll();
      // var ids = pokemons.Select(p => p.Id).Distinct();
      // foreach(var id in ids)
      // {
      //   repository.Add(pokemons.First(p => p.Id == id));
      // }
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseStaticFiles();

      app.UseMvc();

    //   app.Run(async (context) =>
    //   {
    //     await context.Response.WriteAsync("Hello World!");
    //   });
    }
  }
}
