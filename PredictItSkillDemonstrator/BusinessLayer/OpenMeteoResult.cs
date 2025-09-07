using System.Text.Json.Serialization;

namespace PredictItSkillDemonstrator.BusinessLayer;

/// <summary>
/// Hourly data
/// </summary>
public class HourlyData
{
    /// <summary>
    /// Array of temperatures
    /// </summary>
    [JsonPropertyName("temperature_2m")]
    public decimal[]? Temperature { get; set; }

    /// <summary>
    /// Array of times
    /// </summary>
    [JsonPropertyName("time")]
    public DateTime[]? Time { get; set; }
}

/// <summary>
/// Result information from OpenMeteo with only the data we care about
/// </summary>
public class OpenMeteoResult
{
    /// <summary>
    /// Hourly records
    /// </summary>
    [JsonPropertyName("hourly")]
    public required HourlyData Hourly { get; set; }
}
