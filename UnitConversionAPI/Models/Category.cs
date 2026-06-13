namespace UnitConversionAPI.Models;

public class Category
{
    public string? Name { get; set; }
    public bool IsTemperature { get; set; }
    public List<Unit> Units { get; set; } = new();
}
