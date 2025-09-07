using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

using PredictItSkillDemonstrator.BusinessLayer;

namespace PredictItSkillDemonstrator.Controllers;

/// <summary>
/// Controller for the weather forcast.
/// </summary>
[Authorize()]
[ApiController()]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly WeatherHelper _weatherHelper;

    // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
    static readonly string[] scopeRequiredByApi = ["access_as_user"];

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="weatherHelper">Weather helper / business logic.</param>
    public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherHelper weatherHelper)
    {
        this._logger = logger;
        this._weatherHelper = weatherHelper;
    }

    /// <summary>
    /// Retrieves a weather forecast.
    /// </summary>
    /// <returns>A list of weather forecasts for the next few days.</returns>
    [HttpGet()]
    // [AuthorizeForScopes(Scopes = ["access_as_user"])]
    public IEnumerable<WeatherForecast> Get()
    {
        // Should probably use an attribute for this one. See commented out attribute line above.
        HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

        //QUESTION #1 - On the line below add a comment which describes in detail what is happening in the code below

        // Creates a random number generator. Can probably use a static, since the current 
        // implementation is pretty "random". Would also allow for replacing in the future if it
        // were injected using DI.
        var rng = new Random();
        // Base date - today. Question: Does time zone matter?
        var today = DateOnly.FromDateTime(DateTime.Now);

        // Enumerable.Range will generate a list from 1 to 5 in this case, creating
        // a Weather forecast for each one. While this is fine for such a simple case,
        // more complex cases would probably be better by creating an array and then
        // populating the array as part of a loop.
        // Moved generator into helper class.
        return Enumerable.Range(1, 5)
            .Select(index => this._weatherHelper.RandomForecast(rng, today.AddDays(index)));
        // following line removed as unnecessary.
        // .ToArray();

        //END QUESTION #1
    }
}
