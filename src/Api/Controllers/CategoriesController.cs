using Api.Controllers.Common;
using Application.Categories.ChangeCategoryPosition;
using Application.Categories.CreateCategory;
using Application.Categories.ListAllCategories;
using Application.Categories.RemoveCategory;
using Application.Categories.UpdateCategory;
using Application.Common.ServiceHandler;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("categories")]
public class CategoriesController : ApiController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(
        IServiceHandler<CreateCategoryRequest, ErrorOr<Success>> service,
        CreateCategoryRequest request)
    {
        var result = await service.Handler(request);
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(
        IServiceHandler<UpdateCategoryRequest, ErrorOr<Success>> service,
        UpdateCategoryRequest request)
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

    [HttpDelete("remove")]
    public async Task<IActionResult> Remove(
        IServiceHandler<RemoveCategoryRequest, ErrorOr<Success>> service,
        RemoveCategoryRequest request)
    {
        var result = await service.Handler(request);
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }

    [HttpPatch("change-position")]
    public async Task<IActionResult> ChangePosition(
        IServiceHandler<IEnumerable<ChangeCategoryPositionRequest>, ErrorOr<Success>> service,
        IEnumerable<ChangeCategoryPositionRequest> request)
    {
        var result = await service.Handler(request);
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }
}