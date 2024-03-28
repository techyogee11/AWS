## Configuration of the application

Refer this link - https://www.c-sharpcorner.com/article/how-to-implement-amazon-sqs-aws-sqs-in-asp-net-core-project/

Add Security credentials under IAM 
Create access key 
Copy the Access Key ID and Secret access key
Open cmd in Admin
run aws configure command 
aws configure
AWS Access Key ID[None]: AKIAIOSFODNN7EXAMPLE
AWS Secret Access Key[None]: wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
Default region name[None]: us-west-2  
Default output format[None]: json
This will create 2 (config, credentials) files under C:\Users\[username]\.aws folder
Also under IAM user Permission tab add permission to access SQS