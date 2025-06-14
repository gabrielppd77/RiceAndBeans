using ErrorOr;
using MediatR;

namespace Application.Companies.FormData;

public record FormDataQuery : IRequest<ErrorOr<FormDataResult>>;