
using Amazon.S3;
using Amazon.S3.Model;

namespace ServerlessAPI.Services;

public class S3Service : IS3Service
{
    private readonly IAmazonS3 _amazonS3;

    public S3Service(IAmazonS3 amazonS3)
    {
        _amazonS3 = amazonS3;
    }

    public Task<string> GetPreSignedUrlAsync(string bucketName, string fileKey, TimeSpan expire)
    {
        var request = new GetPreSignedUrlRequest
        {            
            BucketName = bucketName,
            Key = fileKey,
            Expires = DateTime.UtcNow.Add(expire)
         };

         return Task.FromResult(_amazonS3.GetPreSignedURL(request));
    }

    public async Task<string?> UploadFileAsnyc(string bucketName, string fileKey, Stream fileStream, string contentType)
    {
        var request = new PutObjectRequest
        {
            BucketName =bucketName,
            Key = fileKey,
            InputStream = fileStream,
            ContentType = contentType
        };

       await _amazonS3.PutObjectAsync(request);

       return fileKey;
    }
}