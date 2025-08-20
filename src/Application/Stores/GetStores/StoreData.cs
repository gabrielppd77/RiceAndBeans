namespace Application.Stores.GetStores;

public record StoreData(
    Guid CompanyId,
    string CompanyName,
    string CompanyPath,
    string? CompanyUrlImage);