using System.Collections.Generic;

namespace Models
{
  public class Filter
  {
    public int Length { get; set; }
    public int Start { get; set; }
    public string SearchText { get; set; }
    public string OrderBy { get; set; }
    public string OrderDir { get; set; }
  }
}