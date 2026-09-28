namespace Application.Products.UpdateProduct;

public record UpdateProductRequest(
    Guid ProductId,
    Guid? CategoryId,
    string Name,
    string? Description,
    decimal Price);