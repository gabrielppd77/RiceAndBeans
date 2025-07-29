namespace Application.Stores.GetStoreData;

public record GetStoreDataResponse(
    Guid Id,
    string Name,
    string? Description,
    string? UrlImage,
    IEnumerable<GetStoreProductResponse> Products);