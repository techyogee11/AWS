using CloudFormationLambda.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;


namespace CloudFormationLambda.Controllers
{
    [Route("api/[controller]")]
    public class TempretureController : ControllerBase
    {
       private readonly ILogger<TempretureController> _logger;
        private readonly ITempratureService _tempratureService;
        public TempretureController(ILogger<TempretureController> logger, ITempratureService tempratureService)
        {
            _logger = logger;
            _tempratureService = tempratureService;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            var temps = await _tempratureService.GetLatestTemparature();
            return Results.Ok(temps);
        }

        [HttpGet("{numObservations}")]
        public async Task<IResult> GetHistory(int numObservations)
        {
            var temps = await _tempratureService.GetTempratureHistory(numObservations);
            return Results.Ok(temps);
        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] string observation)
        {
            var temprature = double.Parse(observation);
            var result = await _tempratureService.LogCurrentTemperature(temprature);
            return Results.Ok(result);
        }
    }

   

}


