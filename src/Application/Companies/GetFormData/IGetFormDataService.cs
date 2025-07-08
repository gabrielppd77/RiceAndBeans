using ErrorOr;

namespace Application.Companies.GetFormData;

public interface IGetFormDataService
{
    Task<ErrorOr<FormDataResponse>> Handle();
}