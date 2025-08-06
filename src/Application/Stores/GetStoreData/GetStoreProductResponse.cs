namespace Application.Stores.GetStoreData;

public record GetStoreProductResponse(
    Guid Id,
    string Name,
    string? Description,
    string? UrlImage,
    decimal Price,
    string CategoryName);