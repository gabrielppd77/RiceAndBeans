using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Users.UploadImage;

public record UploadImageCommand(IFormFile File) : IRequest<ErrorOr<string>>;