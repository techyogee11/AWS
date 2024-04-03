using Amazon.S3;

namespace s3Demo
{
    public static class BucketHelper
    {
       
        public static async Task<bool> DoesBucketExistsAsync(string bucketName, IAmazonS3 _amazonS3)
        {
           return await _amazonS3.DoesS3BucketExistAsync(bucketName);

        }

    }
}
