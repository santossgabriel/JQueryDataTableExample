using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers
{
  [Route("api/[controller]")]
  public class PokemonController : Controller
  {
    private PokemonService _service;
    public PokemonController()
    {
      _service = new PokemonService();
    }

    [HttpGet]
    public object Get(Filter filtro)
    {
      var list = _service.GetAll();
      int total = list.Count;

      list = list.Where(p => string.IsNullOrEmpty(filtro.SearchText) || p.Name.Contains(filtro.SearchText)).ToList();
      int filtrados = list.Count;

      PropertyInfo prop = typeof(Pokemon).GetProperty(filtro.OrderBy);

      if (prop != null)
      {
        if (filtro.OrderDir == "asc")
          list = list.OrderBy(p => prop.GetValue(p, null)).ToList();
        else
          list = list.OrderByDescending(p => prop.GetValue(p, null)).ToList();
      }

      list = list.Skip(filtro.Start).Take(filtro.Length).ToList();
      return new
      {
        data = list,
        recordsTotal = total,
        recordsFiltered = filtrados
      };
    }

    [HttpGet("{id}")]
    public Pokemon Get(int id) => _service.Get(id);

    [HttpPost]
    public void Post(Pokemon p) => _service.Add(p);
  }
}