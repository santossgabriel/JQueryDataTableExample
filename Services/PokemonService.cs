using System.Collections.Generic;
using System.IO;
using System.Linq;
using Models;
using Newtonsoft.Json;

namespace Services
{
  public class PokemonService
  {
    private const string DATA_PATH = "data.json";

    public List<Pokemon> GetAll()
    {
      string text = File.ReadAllText(DATA_PATH);
      return JsonConvert.DeserializeObject<List<Pokemon>>(text);
    }

    public Pokemon Get(int id)
    {
      return GetAll().FirstOrDefault(p => p.Id == id);
    }

    public void Add(Pokemon p)
    {
      var list = GetAll();
      if (p != null && !list.Any(x => p.Id == x.Id))
      {
        list.Add(p);
        File.WriteAllText(DATA_PATH, JsonConvert.SerializeObject(list));
      }
    }
  }
}