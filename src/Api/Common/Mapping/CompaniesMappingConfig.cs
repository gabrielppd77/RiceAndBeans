using Api.Controllers.Companies.Contracts;
using Application.Companies.GetFormData;
using Application.Companies.UpdateFormData;
using Mapster;

namespace Api.Common.Mapping;

public class CompaniesMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FormDataResult, FormDataResponse>();
        config.NewConfig<FormDataRequest, UpdateFormDataCommand>();
    }
}