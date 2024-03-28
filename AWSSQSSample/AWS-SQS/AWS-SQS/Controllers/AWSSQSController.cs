using AWS_SQS.Models;
using AWS_SQS.Service;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace AWS_SQS.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AWSSQSController : Controller
    {

        //#### Refer this link - https://www.c-sharpcorner.com/article/how-to-implement-amazon-sqs-aws-sqs-in-asp-net-core-project/

        //# Add Security credentials under IAM 
        //# Create access key 
        //# Copy the Access Key ID and Secret access key
        //# Open cmd in Admin
        //# run aws configure command 
        //# aws configure
        //# AWS Access Key ID[None]: AKIAIOSFODNN7EXAMPLE
        //# AWS Secret Access Key[None]: wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
        //# Default region name[None]: us-west-2  
        //# Default output format[None]: json
        //# This will create 2 (config, credentials) files under C:\Users\[username]\.aws folder
        //# Also under IAM user Permission tab add permission to access SQS

        private readonly IAWSSQSService _aWSSQSService;

        public AWSSQSController(IAWSSQSService aWSSQSService)
        {
            this._aWSSQSService = aWSSQSService;
        }

        [Route("postMessage")]
        [HttpPost]
        public async Task<IActionResult> PostMessageAsync([FromBody] UserDetail userDetail)
        {
            
            var result = await _aWSSQSService.PostMessageAsync(userDetail);

            return Ok(new { isSuccess = result });
        }

        [Route("getAllMessage")]
        [HttpGet]
        public async Task<IActionResult> GetAllMessagesAsync()
        {
            var result = await _aWSSQSService.GetAllMessagesAsync();

            return Ok(result);
        }

        [Route("deleteMessage")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMessagesAsync(DeleteMessage deleteMessage)
        {
            var result = await _aWSSQSService.DeleteMessageAsync(deleteMessage);

            return Ok(new { isSuccess = result });

        }


    }
}
