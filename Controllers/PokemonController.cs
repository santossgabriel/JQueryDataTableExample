using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using Services;

namespace Controllers
{
  [Route("api/[controller]")]
  public class PokemonController : Controller
  {
    private PokemonRepository _repository;
    public PokemonController()
    {
      _repository = new PokemonRepository();
    }

    [HttpGet]
    public object Get(Filter filter)
    {
      var list = _repository.GetAll(filter);

      // list = list.Where(p => string.IsNullOrEmpty(filtro.SearchText) || p.Name.Contains(filtro.SearchText)).ToList();
      int filtrados = list.Count;

      // PropertyInfo prop = typeof(Pokemon).GetProperty(filtro.OrderBy);

      // if (prop != null)
      // {
      //   if (filtro.OrderDir == "asc")
      //     list = list.OrderBy(p => prop.GetValue(p, null)).ToList();
      //   else
      //     list = list.OrderByDescending(p => prop.GetValue(p, null)).ToList();
      // }

      // list = list.Skip(filtro.Start).Take(filtro.Length).ToList();
      var first = list.First();
      return new
      {
        data = list,
        recordsTotal = first.Total,
        recordsFiltered = first.Filtered
      };
    }

    [HttpGet("{id}")]
    public Pokemon Get(int id) => _repository.Get(id);

    [HttpPost]
    public void Post(Pokemon p) => _repository.Add(p);
  }
}