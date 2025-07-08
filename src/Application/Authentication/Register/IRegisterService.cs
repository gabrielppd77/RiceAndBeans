using Application.Authentication.Common;
using ErrorOr;

namespace Application.Authentication.Register;

public interface IRegisterService
{
    Task<ErrorOr<AuthenticationResponse>> Handle(RegisterRequest request);
}