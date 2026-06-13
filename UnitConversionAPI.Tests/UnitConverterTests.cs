using UnitConversionAPI.Services;
using UnitConversionAPI.Models;
using Xunit;

namespace UnitConversionAPI.Tests;

public class UnitConverterTests
{
    [Fact]
    public void Convert_Length_m_to_cm()
    {
        var converter = new UnitConverter();
        var request = new ConversionRequest { Category = "length", FromUnit = "m", ToUnit = "cm", Value = 2.0 };
        var result = converter.Convert(request);
        Assert.NotNull(result);
        Assert.Equal("m", result.FromUnit, ignoreCase: true);
        Assert.Equal("cm", result.ToUnit, ignoreCase: true);
        Assert.Equal(200.0, result.Output, 5);
    }

    [Fact]
    public void Convert_Temperature_c_to_f()
    {
        var converter = new UnitConverter();
        var request = new ConversionRequest { Category = "temperature", FromUnit = "c", ToUnit = "f", Value = 0.0 };
        var result = converter.Convert(request);
        Assert.NotNull(result);
        Assert.Equal(32.0, result.Output, 5);
    }

    [Fact]
    public void Convert_Weight_kg_to_lb()
    {
        var converter = new UnitConverter();
        var request = new ConversionRequest { Category = "weight", FromUnit = "kg", ToUnit = "lb", Value = 1.0 };
        var result = converter.Convert(request);
        Assert.NotNull(result);
        Assert.InRange(result.Output, 2.2, 2.205);
    }
}
