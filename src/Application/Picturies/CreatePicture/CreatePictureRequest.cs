using Microsoft.AspNetCore.Http;

namespace Application.Picturies.CreatePicture;

public record CreatePictureRequest(
    IFormFile File,
    string Path,
    string EntityType,
    Guid EntityId);