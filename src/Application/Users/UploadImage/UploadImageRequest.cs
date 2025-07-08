using Microsoft.AspNetCore.Http;

namespace Application.Users.UploadImage;

public record UploadImageRequest(IFormFile File);