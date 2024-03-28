## Details

This application send the UserDetails to SQS on AWS, gets/read all the send messages and delete all the messages

## Configuration of the application

Refer this link - https://www.c-sharpcorner.com/article/how-to-implement-amazon-sqs-aws-sqs-in-asp-net-core-project/

1. Add Security credentials under IAM 

2. Create access key 

3. Copy the Access Key ID and Secret access key

4. Open cmd in Admin

5. run aws configure command 

6. aws configure

7. AWS Access Key ID[None]: AKIAIOSFODNN7EXAMPLE

8. AWS Secret Access Key[None]: wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY

9. Default region name[None]: us-west-2  

10. Default output format[None]: json

11. This will create 2 (config, credentials) files under C:\Users\[username]\.aws folder

12. Also under IAM user Permission tab add permission to access SQS
