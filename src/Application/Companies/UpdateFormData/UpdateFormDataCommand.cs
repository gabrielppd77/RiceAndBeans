using ErrorOr;
using MediatR;

namespace Application.Companies.UpdateFormData;

public record UpdateFormDataCommand(string Name, string? Description, string Path) : IRequest<ErrorOr<Unit>>;