using Microsoft.AspNetCore.Http;

namespace Application.Products.UploadImage;

public record UploadImageRequest(Guid ProductId, IFormFile File);