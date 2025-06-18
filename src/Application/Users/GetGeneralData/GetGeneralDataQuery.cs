using ErrorOr;
using MediatR;

namespace Application.Users.GetGeneralData;

public record GetGeneralDataQuery : IRequest<ErrorOr<GeneralDataResult>>;