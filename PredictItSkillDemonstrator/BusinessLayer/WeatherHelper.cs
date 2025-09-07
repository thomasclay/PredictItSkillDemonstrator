using System.Text.Json;

namespace PredictItSkillDemonstrator.BusinessLayer;

/// <summary>
/// Helper class to generate forecasts.
/// </summary>
/// <remarks>Meant to be used using DI.</remarks>
public class WeatherHelper
{
    private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

    //QUESTION #2 - Fill in this function
    /// <summary>
    /// Return the forecasts from forecastsForNextMonth ordered by date
    /// </summary>
    /// <param name="forecastsForNextMonth">A list of forecasts for the month</param>
    /// <param name="coldTempCutOffInFahrenheit">The temperature below or equal to which
    /// is considered cold (in Fahrenheit)</param>
    /// <returns>A list of "cold" weather forecasts.</returns>
    public WeatherForecast[] GetColdForecasts(List<WeatherForecast> forecastsForNextMonth, int coldTempCutOffInFahrenheit)
    {
        return forecastsForNextMonth
            .Where(x => x.TemperatureF < coldTempCutOffInFahrenheit)
            .OrderBy(x => x.Date)
            .ToArray();
    }

    // END QUESTION #2

    // QUESTION #3 - Create another function with the same name below which gets the cold forecasts but defines cold as below 50 degrees

    /// <summary>
    /// Return the forecasts from forecastsForNextMonth ordered by date that are under 50 degrees.
    /// </summary>
    /// <param name="forecastsForNextMonth">A list of forecasts for the month</param>
    /// <returns>A list of "cold" weather forecasts - under 50 degrees.</returns>
    public WeatherForecast[] GetColdForecasts(List<WeatherForecast> forecastsForNextMonth) => GetColdForecasts(
        forecastsForNextMonth,
        50);

    // END QUESTION #3

    // QUESTION #4 - Create a function which calls the Open-Meteo API
    // https://https://open-meteo.com/ and returns the weather temperature forecast for tomorrow
    // afternoon for our Provo office at 86N University Avenue
    // GPS: 40.234813, -111.658159

    /// <summary>
    /// Temperatures, at a specific time.
    /// </summary>
    /// <param name="Time">Date/Time of the temperature</param>
    /// <param name="TemperatureC">Temperature, in centigrade</param>
    public record Temperatures(DateTime Time, decimal TemperatureC);

    /// <summary>
    /// Returns the temperatures for tomorrow afternoon at the PredictIt office in Provo.
    /// </summary>
    /// <remarks>From demo:<br />
    /// https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m&wind_speed_unit=mph&precipitation_unit=inch&start_date=2025-09-08&end_date=2025-09-08<br />
    /// <br />
    /// TODO: Send in the HttpClient using DI. Possible use Refit to generate service so we can use different lat/long, 
    /// date ranges, etc.
    /// </remarks>
    /// <returns>Temperatures for tomorrow afternoon</returns>
    public async Task<IEnumerable<Temperatures>> GetTommorowAfternoonForecastAtOfficeAsync(HttpClient client, CancellationToken cancellationToken = default)
    {
        const decimal longitude = -111.658159m;
        const decimal latitude = 40.234813m;
        var tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&timezone=America%2FDenver&hourly=temperature_2m&wind_speed_unit=mph&precipitation_unit=inch&start_date={tomorrow}&end_date={tomorrow}";

        var httpResult = await client.GetAsync(url, cancellationToken);
        httpResult.EnsureSuccessStatusCode();

        var data = await httpResult.Content.ReadFromJsonAsync<OpenMeteoResult>(cancellationToken);
        if ((data?.Hourly.Time is null) || (data.Hourly.Temperature is null) || (data.Hourly.Time.Length != data.Hourly.Temperature.Length))
        {
            // shouldn't happen
            throw new JsonException("Unexpected result in stream");
        }
        var temperatures = data.Hourly.Time.Zip(data.Hourly.Temperature, (time, temp) => new Temperatures(time, temp));

        var afternoon = new TimeSpan(12, 0, 0);
        return temperatures.Where(t => t.Time.TimeOfDay >= afternoon)
            .OrderBy(t => t.Time);
    }

    //END QUESTION #4

    /// <summary>
    /// Helper function to generate a random forecast
    /// </summary>
    /// <param name="rng">Random number generator</param>
    /// <param name="date">Date of the forecast.</param>
    /// <returns></returns>
    public WeatherForecast RandomForecast(Random rng, DateOnly date)
    {
        return new()
        {
            Date = date,
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)],
        };
    }
}
