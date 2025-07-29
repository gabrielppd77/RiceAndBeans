using Api.Controllers.Common;
using Application.Common.ServiceHandler;
using Application.Stores.GetStoreData;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("stores")]
public class StoresController : ApiController
{
    [AllowAnonymous]
    [HttpGet("get-store-data")]
    public async Task<IActionResult> GetStoreData(
        IServiceHandler<GetStoreDataRequest, ErrorOr<GetStoreDataResponse>> service,
        string companyPath)
    {
        var result = await service.Handler(new GetStoreDataRequest(companyPath));
        return result.Match(
            Ok,
            Problem
        );
    }
}