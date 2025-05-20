using Mapster;
using Application.Authentication.Common;
using Application.Authentication.Login;
using Application.Authentication.Register;
using Api.Controllers.Authentication.Contracts;

namespace Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<RegisterRequest, RegisterCommand>();
		config.NewConfig<LoginRequest, LoginQuery>();
		config.NewConfig<AuthenticationResult, AuthenticationResponse>()
			.Map(dest => dest, src => src.User);
	}
}