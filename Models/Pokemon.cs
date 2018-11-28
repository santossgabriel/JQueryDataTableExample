using System.Collections.Generic;

namespace Models
{
  public class Pokemon
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Experience { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public List<string> Abilities { get; set; }
    public List<string> Sprites { get; set; }
    public List<string> Types { get; set; }
    public List<Stat> Stats { get; set; }
    public int Total { get; set; }
    public int Filtered { get; set; }

    public Pokemon()
    {
      Abilities = new List<string>();
      Sprites = new List<string>();
      Types = new List<string>();
      Stats = new List<Stat>();
    }
  }
}