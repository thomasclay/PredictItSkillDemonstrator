namespace PredictItSkillDemonstrator;

/// <summary>
/// A class to handle the weather forcast
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// Date of the forcase
    /// </summary>
    public required DateOnly Date { get; set; }

    /// <summary>
    /// The original temperature, in Centigrade.
    /// </summary>
    public required int TemperatureC { get; set; }

    /// <summary>
    /// Temperature converted to Fahrenheit - standard calculation
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// A simple summary of the weather forecast.
    /// </summary>
    public required string Summary { get; set; }
}
