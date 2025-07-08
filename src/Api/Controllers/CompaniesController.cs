using Application.Companies.GetFormData;
using Application.Companies.UpdateFormData;
using Application.Companies.UploadImage;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("companies")]
public class CompaniesController : ApiController
{
    [HttpGet("get-form-data")]
    public async Task<IActionResult> GetFormData(IGetFormDataService service)
    {
        var result = await service.Handle();
        return result.Match(
            Ok,
            Problem
        );
    }

    [HttpPut("update-form-data")]
    public async Task<IActionResult> UpdateFormData(IUpdateFormDataService service, UpdateFormDataRequest request)
    {
        var result = await service.Handle(request);
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }

    [HttpPatch("upload-image")]
    public async Task<IActionResult> UploadImage(IUploadImageService service, IFormFile file)
    {
        var result = await service.Handle(new UploadImageRequest(file));
        return result.Match(
            Ok,
            Problem
        );
    }
}