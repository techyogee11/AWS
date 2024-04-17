using Amazon;
using Amazon.StepFunctions;
using Amazon.StepFunctions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepFunctionExeceution
{
    public class StepFunctionWarapper
    {
        private readonly IAmazonStepFunctions _awsStepFunctions;

        public StepFunctionWarapper() { }

        public StepFunctionWarapper(IAmazonStepFunctions awsStepFunctions)
        {
            _awsStepFunctions = awsStepFunctions;
        }

        /// <summary>
        /// Start execution of an AWS Step Functions state machine.
        /// </summary>
        /// <param name="executionName">The name to use for the execution.</param>
        /// <param name="executionJson">The JSON string to pass for execution.</param>
        /// <param name="stateMachineArn">The Amazon Resource Name (ARN) of the Step Functions state machine.</param>
        /// <returns>The Amazon Resource Name (ARN) of the AWS Step Functions execution.</returns>
        public async Task<string> StartExecutionAsync(string executionJson, string stateMachineArn)
        {
            try
            {
                using (var _amazonStepFunctions = GetStepfunctionClient())
                {
                    StartExecutionRequest executionRequest = new StartExecutionRequest
                    {
                        Name = $"{Guid.NewGuid().ToString()}",
                        Input = executionJson,
                        StateMachineArn = stateMachineArn
                    };

                    // response from the StartExecution service method, as returned by StepFunction.
                    var response = await _amazonStepFunctions.StartExecutionAsync(executionRequest);
                    return response.ExecutionArn;
                }
            }
            catch (Exception ex)
            {
                return "Error - " + ex.Message.ToString();
            }
        }

        /// <summary>
        /// Get Object Of AWS Step Function Client
        /// </summary>
        /// <returns>Object Of AWS Step Function Client</returns>
        private AmazonStepFunctionsClient GetStepfunctionClient()
        {
            AmazonStepFunctionsConfig amazonStepFunctionsConfig = new AmazonStepFunctionsConfig { RegionEndpoint = RegionEndpoint.USEast1 };

            string aws_Access_Key = "AKIA3FNPHSP2PY3LKEHT";//Environment.GetEnvironmentVariable("AWS_Access_Key");
            string aws_Access_Key_Secret = "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";//Environment.GetEnvironmentVariable("AWS_Access_Key_Secret");

            if (!string.IsNullOrEmpty(aws_Access_Key) && !string.IsNullOrEmpty(aws_Access_Key_Secret))
            {
                return new AmazonStepFunctionsClient(aws_Access_Key, aws_Access_Key_Secret, amazonStepFunctionsConfig);
            }
            else
            {
                return new AmazonStepFunctionsClient(amazonStepFunctionsConfig);
            }
        }
    }
}