using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ProcessPurchase;

public class Function
{
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public string FunctionHandler(FunctionInput input, ILambdaContext context)
    {
        var response = new Dictionary<string, string>()
 {
     { "TransactionType", input.TransactionType },
     {"TimeStamp", DateTime.UtcNow.ToString("U") },
     {"Message", "Hello from lambada land inside the Process Purchase function" }
 };

        string jsonString = JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented);

        return jsonString;
    }

}

public class FunctionInput
{
    public string TransactionType { get; set; }
}
