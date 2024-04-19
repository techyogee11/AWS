using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLambda.Models
{
    [DynamoDBTable("users")]
    public class User
    {
        [DynamoDBHashKey("email")]
        public string? Email { get; set; }

        [DynamoDBProperty("username")]
        public string? UserName { get; set; }

        [DynamoDBProperty("password")]
        public string? Password { get; set; }
    }
}
