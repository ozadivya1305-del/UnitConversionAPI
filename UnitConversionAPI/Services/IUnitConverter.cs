using UnitConversionAPI.Models;

namespace UnitConversionAPI.Services;

public interface IUnitConverter
{
    ConversionResult? Convert(ConversionRequest request);
}
