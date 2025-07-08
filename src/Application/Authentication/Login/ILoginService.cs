using Application.Authentication.Common;
using ErrorOr;

namespace Application.Authentication.Login;

public interface ILoginService
{
    Task<ErrorOr<AuthenticationResponse>> Handle(LoginRequest request);
}