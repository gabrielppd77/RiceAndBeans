using ErrorOr;
using MediatR;

namespace Application.Companies.GetFormData;

public record GetFormDataQuery : IRequest<ErrorOr<FormDataResult>>;