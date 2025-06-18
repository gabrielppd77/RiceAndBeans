using Api.Controllers.Users.Contracts;
using Application.Users.GetGeneralData;
using Mapster;

namespace Api.Common.Mapping;

public class UsersMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GeneralDataResult, GeneralDataResponse>();
    }
}