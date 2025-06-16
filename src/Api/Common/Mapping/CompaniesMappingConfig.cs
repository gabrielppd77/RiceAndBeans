using Api.Controllers.Companies.Contracts;
using Application.Companies.GetFormData;
using Mapster;

namespace Api.Common.Mapping;

public class CompaniesMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FormDataResult, FormDataResponse>();
    }
}