using System.Collections.Generic;
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
    public object Get(object filtro)
    {
      return new { data = _service.GetAll() };
    }

    [HttpGet("{id}")]
    public Pokemon Get(int id) => _service.Get(id);

    [HttpPost]
    public void Post(Pokemon p) => _service.Add(p);
  }
}