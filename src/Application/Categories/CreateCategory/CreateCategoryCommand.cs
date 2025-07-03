using ErrorOr;
using MediatR;

namespace Application.Categories.CreateCategory;

public record CreateCategoryCommand(string Name) : IRequest<ErrorOr<Unit>>;