using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace SNS_Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public async Task ProcessOrderAsync(Order order)
        {
            var credentials = new BasicAWSCredentials("AKIA3FNPHSP2BEQPWHQF", "JCKYzLdeI7HytIIyvRIzAeNLf3xIrj/MPTK4Zs8x");
            var client = new AmazonSimpleNotificationServiceClient(credentials, Amazon.RegionEndpoint.USEast1);

            var request = new PublishRequest()
            {
                TopicArn = "arn:aws:sns:us-east-1:767556031476:IncomingOrder",
                Message = JsonSerializer.Serialize(order),
                Subject = "New Order Received - " + Guid.NewGuid().ToString()
            };

            var response = await client.PublishAsync(request);
            
        }
    }
}
