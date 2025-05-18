using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace RiceAndBeans.Application.Companies.UploadImage;

public record UploadImageCommand(IFormFile File) : IRequest<ErrorOr<Unit>>;