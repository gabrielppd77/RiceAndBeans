using Microsoft.AspNetCore.Http;

namespace Application.Companies.UploadImage;

public record UploadImageRequest(IFormFile File);