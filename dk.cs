/*public class dk : Character
{
  public List<string> Species { get; set; } = [];
  public override string Display()
  {
    return $"Id: {Id}\nName: {Name}\nDescription: {Description}\nSpecies: {string.Join(", ", Species)}\n";
  }
}*/
public class dk : Character
{
    public string? Species { get; set; }
    
    public override string Display()
    {
        return $"Id: {Id}\nName: {Name}\nDescription: {Description}\nSpecies: {Species}\n";
    }
}