using Application.Common.Services;
using Application.Companies.GetFormData;
using Application.Companies.UpdateFormData;
using Application.Companies.UploadImage;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("companies")]
public class CompaniesController : ApiController
{
    [HttpGet("get-form-data")]
    public async Task<IActionResult> GetFormData(IServiceHandler<Unit, ErrorOr<FormDataResponse>> service)
    {
        var result = await service.Handler(Unit.Value);
        return result.Match(
            Ok,
            Problem
        );
    }

    [HttpPut("update-form-data")]
    public async Task<IActionResult> UpdateFormData(IServiceHandler<UpdateFormDataRequest, ErrorOr<Success>> service,
        UpdateFormDataRequest request)
    {
        var result = await service.Handler(request);
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }

    [HttpPatch("upload-image")]
    public async Task<IActionResult> UploadImage(IServiceHandler<UploadImageRequest, ErrorOr<string>> service,
        IFormFile file)
    {
        var result = await service.Handler(new UploadImageRequest(file));
        return result.Match(
            Ok,
            Problem
        );
    }
}