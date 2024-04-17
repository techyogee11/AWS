using System.Text.Json;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.AspNetCore.Mvc;

namespace cloud_watch_logging.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get(string cityName)
    {
        var count = Random.Shared.Next(5, 15);
        _logger.LogInformation(
            "Get Weather Forecast called for city {cityName} with count of {count}", cityName, count);
        // await LogUsingClient(cityName);
        return Enumerable.Range(1, count).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
    
    [HttpPost]
    public async Task Post(AddWeatherForecastRequest requestData)
    {
        _logger.LogInformation("Received new weather data for city {City} with request {RequestId}",
            requestData.CityName, requestData.Id);
        var client = new AmazonSQSClient();
        var request = new SendMessageRequest()
        {
            QueueUrl = "https://sqs.ap-southeast-2.amazonaws.com/189107071895/youtube-sqs-demo",
            MessageBody = JsonSerializer.Serialize(requestData)
        };
        var response = await client.SendMessageAsync(request);
    }

    private static async Task LogUsingClient(string cityName)
    {
        var logClient = new AmazonCloudWatchLogsClient();
        var logGroupName = "/aws/weather-forecast-app";
        var logStreamName = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
        var existing = await logClient
            .DescribeLogGroupsAsync(new DescribeLogGroupsRequest()
                {LogGroupNamePrefix = logGroupName});
        var logGroupExists = existing.LogGroups.Any(l => l.LogGroupName == logGroupName);
        if (!logGroupExists)
            await logClient.CreateLogGroupAsync(new CreateLogGroupRequest(logGroupName));

        await logClient.CreateLogStreamAsync(new CreateLogStreamRequest(logGroupName, logStreamName));
        await logClient.PutLogEventsAsync(new PutLogEventsRequest()
        {
            LogGroupName = logGroupName,
            LogStreamName = logStreamName,
            LogEvents = new List<InputLogEvent>()
            {
                new() {Message = $"Get Weather Forecast called for city {cityName}", Timestamp = DateTime.UtcNow}
            }
        });
    }
}