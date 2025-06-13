using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Companies.UploadImage;

public record UploadImageCommand(IFormFile File) : IRequest<ErrorOr<string>>;