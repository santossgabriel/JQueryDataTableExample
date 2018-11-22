using System.Collections.Generic;

namespace Models
{
  public class Pokemon
  {
    public List<string> Abilities { get; set; }
    public int Experience { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Sprites { get; set; }
    public List<string> Types { get; set; }
    public List<Stat> Stats { get; set; }
  }
}