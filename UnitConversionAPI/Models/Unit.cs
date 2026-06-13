namespace UnitConversionAPI.Models;

public class Unit
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public double FactorToBase { get; set; }

    // For temperature conversions
    public Func<double, double>? ToBase { get; set; }
    public Func<double, double>? FromBase { get; set; }
}
