﻿using Api.Controllers.Users.Contracts;
using Application.Users.GetGeneralData;
using Application.Users.RemoveAccount;
using Application.Users.UpdateFormData;
using Application.Users.UploadImage;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Users;

[Route("users")]
public class UsersController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpGet("get-general-data")]
    public async Task<IActionResult> GetFormData()
    {
        var query = new GetGeneralDataQuery();

        var authResult = await mediator.Send(query);

        return authResult.Match(
            x => Ok(mapper.Map<GeneralDataResponse>(x)),
            Problem
        );
    }

    [HttpPut("update-form-data")]
    public async Task<IActionResult> UpdateFormData(FormDataRequest request)
    {
        var command = mapper.Map<UpdateFormDataCommand>(request);

        var authResult = await mediator.Send(command);

        return authResult.Match(
            _ => NoContent(),
            Problem
        );
    }

    [HttpDelete("remove-account")]
    public async Task<IActionResult> RemoveAccount(string password)
    {
        var command = new RemoveAccountCommand(password);

        var authResult = await mediator.Send(command);

        return authResult.Match(
            _ => NoContent(),
            Problem
        );
    }

    [HttpPatch("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var command = new UploadImageCommand(file);

        var authResult = await mediator.Send(command);

        return authResult.Match(
            Ok,
            Problem
        );
    }
}