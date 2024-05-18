using System.Text.Json;
using ArchTestPOC.Attributes;
using ArchTestPOC.Services;

namespace ArchTestPOC.Tests;

public class SerializeTests
{
    [Fact]
    [SerializeTest(typeof(WeatherForecast))]
    public void TestIfWeatherForecastCorrectlySerialize()
    {
        var weatherForecast = new WeatherForecast(DateOnly.FromDateTime(DateTime.UtcNow), 25, "Sunny");

        var jsonString = JsonSerializer.Serialize(weatherForecast);

        var weatherForecastAfterDeserialize = JsonSerializer.Deserialize<WeatherForecast>(jsonString);
        
        Assert.Equal(weatherForecast, weatherForecastAfterDeserialize);
    }
}