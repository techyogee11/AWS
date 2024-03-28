using Amazon.SQS.Model;
using AWS_SQS.Helpers;
using AWS_SQS.Models;
using Newtonsoft.Json;

namespace AWS_SQS.Service
{
    public class AWSSQSService : IAWSSQSService
    {

        private readonly IAWSSQSHelper _AWSSQSHelper;

        public AWSSQSService(IAWSSQSHelper aWSSQSHelper)
        {
            _AWSSQSHelper = aWSSQSHelper;
        }

        public async Task<bool> DeleteMessageAsync(DeleteMessage deleteMessage)
        {
            try
            {
                return await _AWSSQSHelper.DeleteMessageAsync(deleteMessage.ReceiptHandle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<AllMessage>> GetAllMessagesAsync()
        {
           List<AllMessage> allMessages = new List<AllMessage>();

            try
            {
                List<Message> messages = await _AWSSQSHelper.ReceiveMessageAsync();

                allMessages = messages.Select(m => new AllMessage { MessageId= m.MessageId, 
                                ReceiptHandle= m.ReceiptHandle, 
                                UserDetail = JsonConvert.DeserializeObject<UserDetail>(m.Body) }).ToList();

                return allMessages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PostMessageAsync(User user)
        {
            try
            {
                UserDetail userDetail = new UserDetail()
                {
                    Id = new Random().Next(999999999),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Emaild = user.Emaild,
                    CratedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow
                };
                return await _AWSSQSHelper.SendMessageAsync(userDetail);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
