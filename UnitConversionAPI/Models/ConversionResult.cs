namespace UnitConversionAPI.Models;

public class ConversionResult
{
    public string? Category { get; set; }
    public string? FromUnit { get; set; }
    public string? ToUnit { get; set; }
    public double Input { get; set; }
    public double Output { get; set; }
}
