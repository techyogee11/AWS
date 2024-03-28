using Amazon.SQS;
using Amazon.SQS.Model;
using AWS_SQS.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace AWS_SQS.Helpers
{

    public class AWSSQSHelper : IAWSSQSHelper
    {
        private readonly IAmazonSQS _sqs;
        private readonly ServiceConfiguration _serviceConfiguration;

        public AWSSQSHelper(IAmazonSQS amazonSQS, IOptions<ServiceConfiguration> settings)
        {
            this._sqs = amazonSQS;
            this._serviceConfiguration = settings.Value;
        }
        public async Task<bool> DeleteMessageAsync(string messageReceiptHandle)
        {
            try
            {
                var deleteResult = await _sqs.DeleteMessageAsync(_serviceConfiguration.AWSSQS.QueueUrl, messageReceiptHandle);
                return deleteResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Message>> ReceiveMessageAsync()
        {
            try
            {
                //Create New instance
                var request = new ReceiveMessageRequest
                {
                    QueueUrl = _serviceConfiguration.AWSSQS.QueueUrl,
                    MaxNumberOfMessages = 10,
                    WaitTimeSeconds = 5
                };

                //Check if there are any new messages available to process
                var result = await _sqs.ReceiveMessageAsync(request);

                return result.Messages.Any() ? result.Messages : new List<Message>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SendMessageAsync(UserDetail userDetail)
        {
            try
            {
                string message = JsonConvert.SerializeObject(userDetail);

                var sendRequest = new SendMessageRequest(_serviceConfiguration.AWSSQS.QueueUrl, message);

                //Post message or payload ro queue
                var sendResult = await _sqs.SendMessageAsync(sendRequest);
                return sendResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
