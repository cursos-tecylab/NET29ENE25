namespace ServerlessAPI.Controllers;

public record CreateSedeRequest
(
    string Name,
    string? ImageBase64
);