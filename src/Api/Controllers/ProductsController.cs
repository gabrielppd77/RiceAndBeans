using Api.Controllers.Common;
using Application.Common.ServiceHandler;
using Application.Products.CreateProduct;
using Application.Products.ListAllProducts;
using Application.Products.RemoveProduct;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("products")]
public class ProductsController : ApiController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(
        IServiceHandler<CreateProductRequest, ErrorOr<Success>> service,
        CreateProductRequest request)
    {
        var result = await service.Handler(request);
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }

    [HttpGet("list-all")]
    public async Task<IActionResult> ListAll(IServiceHandler<Unit, IEnumerable<ProductResponse>> service)
    {
        var result = await service.Handler(Unit.Value);
        return Ok(result);
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> Remove(
        IServiceHandler<RemoveProductRequest, ErrorOr<Success>> service,
        RemoveProductRequest request)
    {
        var result = await service.Handler(request);
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }
}