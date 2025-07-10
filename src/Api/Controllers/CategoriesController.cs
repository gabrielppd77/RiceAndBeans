using Api.Controllers.Common;
using Application.Categories.CreateCategory;
using Application.Categories.ListAllCategories;
using Application.Common.ServiceHandler;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("categories")]
public class CategoriesController : ApiController
{
    [HttpPost("create")]
    public async Task<IActionResult> Register(
        IServiceHandler<CreateCategoryRequest, ErrorOr<Success>> service,
        CreateCategoryRequest request)
    {
        var result = await service.Handler(request);
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }

    [HttpGet("list-all")]
    public async Task<IActionResult> ListAll(IServiceHandler<Unit, IEnumerable<CategoryResponse>> service)
    {
        var result = await service.Handler(Unit.Value);
        return Ok(result);
    }
}