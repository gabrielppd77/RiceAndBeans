using Api.Controllers.Common;
using Application.Common.Services;
using Application.Users.GetGeneralData;
using Application.Users.RemoveAccount;
using Application.Users.UpdateFormData;
using Application.Users.UploadImage;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("users")]
public class UsersController : ApiController
{
    [HttpGet("get-general-data")]
    public async Task<IActionResult> GetFormData(IServiceHandler<Unit, ErrorOr<GeneralDataResponse>> service)
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

    [HttpDelete("remove-account")]
    public async Task<IActionResult> RemoveAccount(IServiceHandler<RemoveAccountRequest, ErrorOr<Success>> service,
        string password)
    {
        var result = await service.Handler(new RemoveAccountRequest(password));
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