## SNS

1. SNS stands for Simple Notification Service. It is a fully managed pub/sub messaging service provided by Amazon Web Services (AWS)

2. It enables the decoupling or separating of microservices, distributed systems, and serverless applications, thus enhancing the capability of these systems.

3. SNS delivers messages instantly to subscribing endpoints.

4. It enables you to send individual messages or "fanout" messages to a large number of recipients.

5. It is flexible and can deliver messages to email, mobile devices (SMS, push), Amazon SQS queues, or any HTTP endpoint.

6. SNS supports multiple protocols including Amazon SQS, HTTP/S, email, SMS, Lambda, and mobile push notifications.

7. It allows automatic scaling of applications by delivering messages to multiple SQS queues simultaneously.

8. SNS simplifies the management of messaging applications by offloading the administrative burden and heavy lifting tasks.

9. It ensures message delivery with redundancy across multiple regions and availability zones.

10. SNS provides robust security and privacy by encrypting sensitive data at rest and in transit.

11. It also supports access control policies, and AWS Identity and Access Management for user and resource security.

12. With SNS, you can filter messages based on message attributes so that subscribers receive only the messages of interest.

13. SNS supports the ability to fan out notifications to end users via mobile push, SMS, and email which can be leveraged in multi-channel marketing strategies.

14. It is cost-effective as you only pay for what you use. You don’t need to pay any up-front commitments or fixed expenses. 

15. For developers building applications, SNS provides APIs and an easy-to-use management console.

## Flow of the application

Through SNS sample we are posting the sample order data to SNS and it will send the email.
