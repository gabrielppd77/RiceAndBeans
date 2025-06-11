using ErrorOr;
using MediatR;

namespace Application.Project.ApplyMigration;

public record ApplyMigrationCommand(string? Token) : IRequest<ErrorOr<Unit>>;