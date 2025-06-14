using Api.Controllers.Companies.Contracts;
using Application.Companies.FormData;
using Application.Companies.UploadImage;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Companies;

[Route("companies")]
public class CompaniesController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public CompaniesController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("get-form-data")]
    public async Task<IActionResult> GetFormData()
    {
        var query = new FormDataQuery();

        var authResult = await _mediator.Send(query);

        return authResult.Match(
            x => Ok(_mapper.Map<FormDataResponse>(x)),
            Problem
        );
    }

    [HttpPatch("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var command = new UploadImageCommand(file);

        var authResult = await _mediator.Send(command);

        return authResult.Match(
            Ok,
            Problem
        );
    }
}