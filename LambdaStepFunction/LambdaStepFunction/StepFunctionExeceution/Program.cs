
using Amazon.StepFunctions;
using StepFunctionExeceution;
using Newtonsoft.Json;

IAmazonStepFunctions _client = null!;
StepFunctionWarapper _wrapper = null!;


_client = new AmazonStepFunctionsClient();
_wrapper = new StepFunctionWarapper(_client);

var input = 1;
Console.WriteLine("Please enter 1 for PURCHASE OR 2 for REFUND. Default is 1");

input = Convert.ToInt32(Console.ReadLine());

Trasaction trasaction = new Trasaction()
{
    TransactionType = (input == 1) ? "PURCHASE" : "REFUND"
};

string exceutionString = JsonConvert.SerializeObject(trasaction);
string stateArn = "arn:aws:states:us-east-1:767556031476:stateMachine:CreditCardProcess";

var respomse = await _wrapper.StartExecutionAsync(exceutionString, stateArn);

if (!respomse.Contains("Error"))
{
    Console.WriteLine("Success");
}
else
{
    Console.WriteLine(respomse.ToString());
}

Console.ReadLine();



public class Trasaction
{ 
    public string TransactionType { get; set; }

}