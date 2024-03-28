using Amazon.SQS.Model;
using AWS_SQS.Models;

namespace AWS_SQS.Helpers
{
    public interface IAWSSQSHelper
    {
        Task<bool> SendMessageAsync(UserDetail userDetail);
        Task<List<Message>> ReceiveMessageAsync();
        Task<bool> DeleteMessageAsync(string messageReceiptHandle);
    }
}
