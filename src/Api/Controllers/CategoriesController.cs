using Application.Categories.CreateCategory;
using Application.Categories.ListAllCategories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("categories")]
public class CategoriesController : ApiController
{
    [HttpPost("create")]
    public async Task<IActionResult> Register(ICreateCategoryService service, CreateCategoryRequest request)
    {
        var result = await service.Handler(request);
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }

    [HttpGet("list-all")]
    public async Task<IActionResult> ListAll(IListAllCategoriesService service)
    {
        var result = await service.Handle();
        return Ok(result);
    }
}