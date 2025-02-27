using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using System.Text.Json;

namespace ImageProcessingFunctions
{
    public class UploadImageFunction
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<UploadImageFunction> _logger;
        private const string ContainerName = "images";

        public UploadImageFunction(BlobServiceClient blobServiceClient, ILogger<UploadImageFunction> logger)
        {
            _blobServiceClient = blobServiceClient;
            _logger = logger;
        }

        [Function("UploadImageFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            if (!req.HasFormContentType || !req.Form.Files.Any())
                return new BadRequestObjectResult("Invalid request. Upload an image file");

            var file = req.Form.Files[0];

            try
            {
                using var imageStream = file.OpenReadStream();

                var imageInfo = Image.Identify(imageStream);

                if (imageInfo == null)
                {
                    return new BadRequestObjectResult("Invalid image file.");
                }

                imageStream.Position = 0;

                var blobContainer = _blobServiceClient.GetBlobContainerClient(ContainerName);
                await blobContainer.CreateIfNotExistsAsync();

                var blobClient = blobContainer.GetBlobClient(file.FileName);
                await blobClient.UploadAsync(imageStream, true);

                _logger.LogInformation($"Image {file.FileName} uploaded.");
                return new OkObjectResult($"Images {file.FileName} uploaded successfully.");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error uploading file: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
