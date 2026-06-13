using Microsoft.AspNetCore.Mvc;
using UnitConversionAPI.Models;
using UnitConversionAPI.Services;

namespace UnitConversionAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversionController : ControllerBase
{
    private readonly IUnitConverter _converter;

    public ConversionController(IUnitConverter converter)
    {
        _converter = converter;
    }

    [HttpPost("convert")]
    public IActionResult Convert([FromBody] ConversionRequest request)
    {
        if (request == null) return BadRequest("Request body required");
        if (string.IsNullOrWhiteSpace(request.Category)) return BadRequest("Category is required");
        if (string.IsNullOrWhiteSpace(request.FromUnit)) return BadRequest("FromUnit is required");
        if (string.IsNullOrWhiteSpace(request.ToUnit)) return BadRequest("ToUnit is required");

        var result = _converter.Convert(request);
        if (result == null) return BadRequest("Invalid conversion request or unsupported units/category");

        return Ok(result);
    }

    [HttpGet("units/{category}")]
    public IActionResult GetUnits(string category)
    {
        if (string.IsNullOrWhiteSpace(category)) return BadRequest("category required");
        // Expose simple unit list
        var field = category.ToLowerInvariant();
        var list = new List<object>();
        switch (field)
        {
            case "length":
                list.AddRange(new[] { "m", "cm", "mm", "km", "ft", "in", "yd" });
                break;
            case "weight":
                list.AddRange(new[] { "kg", "g", "mg", "lb", "oz" });
                break;
            case "temperature":
                list.AddRange(new[] { "c", "f", "k" });
                break;
            default:
                return NotFound();
        }

        return Ok(list);
    }
}
