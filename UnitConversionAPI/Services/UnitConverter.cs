using UnitConversionAPI.Models;

namespace UnitConversionAPI.Services;

public class UnitConverter : IUnitConverter
{
    private readonly Dictionary<string, Category> _categories;

    public UnitConverter()
    {
        _categories = new Dictionary<string, Category>(StringComparer.OrdinalIgnoreCase)
        {
            { "length", CreateLengthCategory() },
            { "temperature", CreateTemperatureCategory() },
            { "weight", CreateWeightCategory() }
        };
    }

    public ConversionResult? Convert(ConversionRequest request)
    {
        if (request == null) return null;
        if (string.IsNullOrWhiteSpace(request.FromUnit) || string.IsNullOrWhiteSpace(request.ToUnit)) return null;
        if (!_categories.TryGetValue(request.Category ?? "", out var category)) return null;

        var from = category.Units.FirstOrDefault(u => string.Equals(u.Code, request.FromUnit, StringComparison.OrdinalIgnoreCase));
        var to = category.Units.FirstOrDefault(u => string.Equals(u.Code, request.ToUnit, StringComparison.OrdinalIgnoreCase));
        if (from == null || to == null) return null;

        // For temperature we use converters; for others we use factor to base unit
        double baseValue;
        if (category.IsTemperature)
        {
            baseValue = from.ToBase(request.Value);
        }
        else
        {
            baseValue = request.Value * from.FactorToBase;
        }

        double resultValue;
        if (category.IsTemperature)
        {
            resultValue = to.FromBase(baseValue);
        }
        else
        {
            resultValue = baseValue / to.FactorToBase;
        }

        return new ConversionResult
        {
            FromUnit = from.Code,
            ToUnit = to.Code,
            Input = request.Value,
            Output = resultValue,
            Category = category.Name
        };
    }

    private static Category CreateLengthCategory()
    {
        // Base unit: meter
        return new Category
        {
            Name = "length",
            IsTemperature = false,
            Units = new List<Unit>
            {
                new Unit { Code = "m", Name = "Meter", FactorToBase = 1.0 },
                new Unit { Code = "cm", Name = "Centimeter", FactorToBase = 0.01 },
                new Unit { Code = "mm", Name = "Millimeter", FactorToBase = 0.001 },
                new Unit { Code = "km", Name = "Kilometer", FactorToBase = 1000.0 },
                new Unit { Code = "ft", Name = "Foot", FactorToBase = 0.3048 },
                new Unit { Code = "in", Name = "Inch", FactorToBase = 0.0254 },
                new Unit { Code = "yd", Name = "Yard", FactorToBase = 0.9144 }
            }
        };
    }

    private static Category CreateWeightCategory()
    {
        // Base unit: kilogram
        return new Category
        {
            Name = "weight",
            IsTemperature = false,
            Units = new List<Unit>
            {
                new Unit { Code = "kg", Name = "Kilogram", FactorToBase = 1.0 },
                new Unit { Code = "g", Name = "Gram", FactorToBase = 0.001 },
                new Unit { Code = "mg", Name = "Milligram", FactorToBase = 0.000001 },
                new Unit { Code = "lb", Name = "Pound", FactorToBase = 0.45359237 },
                new Unit { Code = "oz", Name = "Ounce", FactorToBase = 0.028349523125 }
            }
        };
    }

    private static Category CreateTemperatureCategory()
    {
        return new Category
        {
            Name = "temperature",
            IsTemperature = true,
            Units = new List<Unit>
            {
                new Unit { Code = "c", Name = "Celsius", ToBase = v => v + 273.15, FromBase = v => v - 273.15 }, // base: Kelvin
                new Unit { Code = "f", Name = "Fahrenheit", ToBase = v => (v + 459.67) * 5.0/9.0, FromBase = v => v * 9.0/5.0 - 459.67 },
                new Unit { Code = "k", Name = "Kelvin", ToBase = v => v, FromBase = v => v }
            }
        };
    }
}
