using Amazon.Lambda.Core;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DynamoDbCrudLambda;

public class Function
{
    private readonly AmazonDynamoDBClient _dynamoDbClient;
    private readonly DynamoDBContext _context;

    public Function()
    {
        _dynamoDbClient = new AmazonDynamoDBClient();
        _context = new DynamoDBContext(_dynamoDbClient);
    }

    // public async Task<string> CreateProductAsync(Product product)
    // {
    //     await _context.SaveAsync(product);
    //     return $"Product {product.ProductId} created sucessfully";
    // }

    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
    {
        return new APIGatewayProxyResponse
        {
            Body = JsonSerializer.Serialize("body"),
            StatusCode = 200,
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }

}

public class Product
{
    public string ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}