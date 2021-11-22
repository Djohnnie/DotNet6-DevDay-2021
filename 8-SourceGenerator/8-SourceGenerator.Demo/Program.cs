using HttpClient = _8_SourceGenerator.Demo.HttpClient;
using System.Text.Json;

HttpClient client = new HttpClient();
var result = await client.GetWeatherForecast("http://localhost:5000/");

Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));