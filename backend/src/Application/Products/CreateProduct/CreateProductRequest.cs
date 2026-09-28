namespace Application.Products.CreateProduct;

public record CreateProductRequest(Guid? CategoryId, string Name, string? Description, decimal Price);