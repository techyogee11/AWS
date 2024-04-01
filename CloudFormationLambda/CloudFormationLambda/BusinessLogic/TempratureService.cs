using CloudFormationLambda.Models;
using System.Security.Cryptography;

namespace CloudFormationLambda.BusinessLogic
{
    public interface ITempratureService
    {
        Task<bool> LogCurrentTemperature(double temperature);
        Task<double> GetLatestTemparature();
        Task<List<TempratureLog>> GetTempratureHistory(int numObersarvations);

    }
    public class TempratureService : ITempratureService
    {

        private readonly ILogger<TempratureService> _logger;

        public TempratureService(ILogger<TempratureService> logger)
        {
            _logger = logger;
        }
        public async Task<double> GetLatestTemparature()
        {
            var random = new Random();
            var temprature = RandomNumberBetween(68, 74);
            _logger.LogDebug("Observed Temprature: {Temprature} ", temprature.ToString("#.##"));

            return await Task.Run(() => Math.Round(temprature, 2));
        }

        public async Task<List<TempratureLog>> GetTempratureHistory(int numObersarvations)
        {
            const int maxLimit = 1000;
            var limit = Math.Min(numObersarvations, maxLimit);
            var id = new Random();
            var history = new List<TempratureLog>();
            for (int i = 0; i < numObersarvations; i++)
            {
                var temprature = RandomNumberBetween(68, 74);
                history.Add(new TempratureLog(id.Next().ToString(), temprature,DateTime.UtcNow));
            }
            return await Task.Run(() => history);
        }

        public async Task<bool> LogCurrentTemperature(double temperature)
        {
            _logger.LogDebug("Observed Tempreture: {Temprature}", temperature.ToString("#.##"));
            return await Task.Run(() => true );

        }

        private static double RandomNumberBetween(double minValue, double maxValue) {

            var random = new Random();
            var next = random.NextDouble();

            return minValue + next * (maxValue  - minValue);

        }
    }
}
