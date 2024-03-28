using AWS_SQS.Models;
using AWS_SQS.Helpers;
using AWS_SQS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_SQS.Service
{
    public interface IAWSSQSService
    {
        Task<bool> PostMessageAsync(User user);
        Task<List<AllMessage>> GetAllMessagesAsync();

        Task<bool> DeleteMessageAsync(DeleteMessage deleteMessage);

    }
}
