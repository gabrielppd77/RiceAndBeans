using Mapster;

using Api.Controllers.RecoverPassword.Contracts;

using Application.RecoverPassword.ResetPassword;

namespace Api.Common.Mapping;

public class RecoverPasswordMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ResetRequest, ResetPasswordCommand>();
    }
}
