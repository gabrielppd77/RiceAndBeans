namespace Application.Products.GetProduct;

public record GetProductResponse(
    Guid Id,
    string Name,
    string? Description,
    string? UrlImage,
    decimal Price,
    Guid? CategoryId);