using Api.Controllers.Companies.Contracts;
using Application.Companies.GetFormData;
using Application.Companies.UpdateFormData;
using Application.Companies.UploadImage;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Companies;

[Route("companies")]
public class CompaniesController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpGet("get-form-data")]
    public async Task<IActionResult> GetFormData()
    {
        var query = new GetFormDataQuery();

        var result = await mediator.Send(query);

        return result.Match(
            x => Ok(mapper.Map<FormDataResponse>(x)),
            Problem
        );
    }

    [HttpPut("update-form-data")]
    public async Task<IActionResult> UpdateFormData(FormDataRequest request)
    {
        var command = mapper.Map<UpdateFormDataCommand>(request);

        var result = await mediator.Send(command);

        return result.Match(
            _ => NoContent(),
            Problem
        );
    }

    [HttpPatch("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var command = new UploadImageCommand(file);

        var result = await mediator.Send(command);

        return result.Match(
            Ok,
            Problem
        );
    }
}