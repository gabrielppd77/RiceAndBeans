namespace Application.Products.ListAllProducts;

public record ProductResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    string? CategoryName);