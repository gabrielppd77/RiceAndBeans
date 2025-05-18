using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiceAndBeans.Application.Authentication.RemoveAccount;
using RiceAndBeans.Application.Companies.UploadImage;

namespace RiceAndBeans.Api.Controllers;

[Route("[controller]")]
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
            authResult => Ok(),
            Problem
        );
    }
}
