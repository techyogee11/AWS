﻿using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using s3Demo.Models;

namespace s3Demo.Controllers
{
    [Route("api/files")]
    public class FileController : Controller
    {
        private readonly IAmazonS3 _amazonS3;
        public FileController(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFileAsync(IFormFile file, string bucketName, string? prefix)
        {

            try
            {
                var bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
                if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");

                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}",
                    InputStream = file.OpenReadStream()

                };
                request.Metadata.Add("Content-Type", file.ContentType);
                await _amazonS3.PutObjectAsync(request);
                return Ok($"File{prefix}/{file.FileName} uploaded to S3 successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllFilesAsync(string bucketName, string? prefix)
        {
            try
            {
                var bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
                if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
                var request = new ListObjectsV2Request()
                {
                    BucketName = bucketName,
                    Prefix = prefix
                };
                var result = await _amazonS3.ListObjectsV2Async(request);
                var s3Objects = result.S3Objects.Select(s =>
                {
                    var urlRequest = new GetPreSignedUrlRequest()
                    {
                        BucketName = bucketName,
                        Key = s.Key,
                        Expires = DateTime.UtcNow.AddMinutes(1)
                    };
                    return new S3ObjectDto()
                    {
                        Name = s.Key,
                        PresingedUrl = _amazonS3.GetPreSignedURL(urlRequest)
                    };
                });

                return Ok(s3Objects);
            }
            catch (Exception ex)
            {
                throw ex;   
            }
        }

        [HttpGet("get-by-key")]
        public async Task<IActionResult> GetFileByKeyAsync(string bucketName, string key)
        {
            try
            {
                var bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
                
                if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
                var s3Object = await _amazonS3.GetObjectAsync(bucketName, key);
                return File(s3Object.ResponseStream, s3Object.Headers.ContentType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFileAsync(string bucketName, string key)
        {
            var bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
            await _amazonS3.DeleteObjectAsync(bucketName, key);
            return NoContent();
        }
    }
}
