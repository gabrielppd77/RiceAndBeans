using ErrorOr;

namespace Application.Companies.UpdateFormData;

public interface IUpdateFormDataService
{
    Task<ErrorOr<Success>> Handle(UpdateFormDataRequest request);
}