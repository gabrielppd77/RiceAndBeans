using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using MediatR;
using Application.Companies.UploadImage;

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

    [HttpPatch("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var command = new UploadImageCommand(file);

        var authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => NoContent(),
            Problem
        );
    }
}