# UnitConversionAPI

A simple ASP.NET Core Web API for converting values between units in categories: length, temperature, and weight.

Run locally:

1. Ensure .NET 10 SDK and Visual Studio 2022 (or later) are installed.
2. Testing is done using Postman to send requests to the API.
3. In repository root run: dotnet run --project UnitConversionAPI
4. The API will be available at https://localhost:5001  or the URL shown in console 
5. Use POST https://localhost:7214/api/conversion/convert endpoint.Make sure to add the port number which you see in the console when you run the project.
6.In Postman, set the request body to JSON format and provide the category, fromUnit, toUnit, and value for conversion.
7.In postman,set Accept header to application/json and Content-Type header to application/json.
8.In postman set Authorization as No Auth as the API does not require authentication.
9.There is one file called UnitConversionAPI.Tests which contains unit tests for the conversion logic.
You can run these tests using the Test Explorer in Visual Studio or using the command line with dotnet test.



Example request:

POST https://localhost:7214/api/conversion/convert
Content-Type: application/json

{"category":"weight","fromUnit":"g","toUnit":"kg","value":1000}

OUTPUT will be like this:
{
	"category": "weight",
	"fromUnit": "g",
	"toUnit": "kg",
	"value": 1000,
	"convertedValue": 1
}

Design notes:
- Units and conversion factors are hardcoded for this exercise.
- Temperature conversions use Kelvin as base with functions for conversion.
- Service IUnitConverter is registered as singleton for simplicity.
- The design separates models, service, and controller for maintainability.
- Test project uses xUnit for unit testing the conversion logic.
- The API is designed to be simple and straightforward, with hardcoded conversion factors for demonstration purposes. It can be extended to support more units and categories as needed.
-Also added error handling for invalid categories, units, and values to ensure the API responds gracefully to incorrect input.
-The API uses a service-oriented architecture, with a dedicated service for handling the conversion logic, making it easy to maintain and extend in the future.
-The API is designed to be stateless, allowing for scalability and ease of deployment in various environments.
-The API follows RESTful principles, making it easy to integrate with other applications and services that require unit conversion functionality.
-The API is built using ASP.NET Core, leveraging its features for building robust and scalable web APIs, including dependency injection, middleware, and routing.
-Also included comprehensive unit tests to ensure the correctness of the conversion logic and to facilitate future maintenance and enhancements of the API.
-Also added one word file called APITestResult which contains the screenshots of the API testing using Postman, demonstrating the successful conversion of units and the handling of error cases. This document serves as a reference for the expected behavior of the API and can be used for future testing and validation purposes.

