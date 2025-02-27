using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImageProcessingFunctions
{
    public class GenerateThumbnailFunction
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<GenerateThumbnailFunction> _logger;
        private const string SourceContainer = "images";
        private const string TargetContainer = "thumbnails";

        public GenerateThumbnailFunction(BlobServiceClient blobServiceClient, ILogger<GenerateThumbnailFunction> logger)
        {
            _blobServiceClient = blobServiceClient;
            _logger = logger;
        }

        [Function("GenerateThumbnailFunction")]
        public async Task Run([BlobTrigger("images/{name}", Connection = "AzureWebJobsStorage")] Stream imageStream, string name)
        {
            var sourceContainer = _blobServiceClient.GetBlobContainerClient(SourceContainer);
            var targetContainer = _blobServiceClient.GetBlobContainerClient(TargetContainer);
            await targetContainer.CreateIfNotExistsAsync();

            var thumbnailBlob = targetContainer.GetBlobClient(name);
            using var image = Image.Load(imageStream);
            image.Mutate(x => x.Resize(100, 100).Grayscale());

            await using var outputStream = new MemoryStream();
            image.SaveAsJpeg(outputStream);
            outputStream.Position = 0;

            await thumbnailBlob.UploadAsync(outputStream, true);

            _logger.LogInformation($"Thumbnail generated for {name}");
        }
    }
}
