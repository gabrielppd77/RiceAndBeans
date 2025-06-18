using ErrorOr;
using MediatR;

namespace Application.Users.UpdateFormData;

public record UpdateFormDataCommand(string Name) : IRequest<ErrorOr<Unit>>;