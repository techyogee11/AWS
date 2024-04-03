using Amazon.S3;
using Microsoft.AspNetCore.Mvc;

namespace s3Demo.Controllers
{
    [Route("api/buckets")]
    [ApiController]
    public class BucketsController : ControllerBase
    {
        private readonly IAmazonS3 _amazonS3;
        public BucketsController(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CraeteBucketResult(string bucketName)
        {
            try
            {
                var bucketExists = await BucketHelper.DoesBucketExistsAsync(bucketName, _amazonS3);
                if (bucketExists) return BadRequest($"Bucket {bucketName} already exists.");
                await _amazonS3.PutBucketAsync(bucketName);
                return Ok($"Bucket {bucketName} created");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBucketAsync()
        { 
            var data = await _amazonS3.ListBucketsAsync();
            var bucket = data.Buckets.Select(b => { return b.BucketName;  });
            return Ok(bucket);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteBucketAsync(string bucketName)
        {
            await _amazonS3.DeleteBucketAsync(bucketName);
            return NoContent();
        }

    }
}
