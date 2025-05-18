using RiceAndBeans.Application.Authentication.Register;
using RiceAndBeans.Application.Authentication.Common;
using RiceAndBeans.Application.Authentication.Login;
using RiceAndBeans.Contracts.Authentication;
using Mapster;

namespace RiceAndBeans.Api.Common.Mapping;

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