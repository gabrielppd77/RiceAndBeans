using Api.Controllers.Categories.Contracts;
using Application.Categories.CreateCategory;
using Application.Categories.ListAllCategories;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Categories;

[Route("categories")]
public class CategoriesController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpPost("create")]
    public async Task<IActionResult> Register(CreateCategoryRequest request)
    {
        var command = mapper.Map<CreateCategoryCommand>(request);

        var authResult = await mediator.Send(command);

        return authResult.Match(
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