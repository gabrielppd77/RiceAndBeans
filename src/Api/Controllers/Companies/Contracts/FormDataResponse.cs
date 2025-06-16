namespace Api.Controllers.Companies.Contracts;

public record FormDataResponse(string Name, string? Description, string Path, string? UrlImage);