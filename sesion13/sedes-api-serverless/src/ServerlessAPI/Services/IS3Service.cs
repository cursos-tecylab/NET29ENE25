namespace ServerlessAPI.Services;

public interface IS3Service
{
    Task<string?> UploadFileAsnyc(string bucketName, string fileKey, Stream fileStream, string contentType);
    Task<string> GetPreSignedUrlAsync(string bucketName, string fileKey, TimeSpan expire);
}