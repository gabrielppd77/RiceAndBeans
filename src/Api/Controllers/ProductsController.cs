using Api.Controllers.Common;
using Application.Common.ServiceHandler;
using Application.Positions.ChangePosition;
using Application.Products.CreateProduct;
using Application.Products.ListAllProducts;
using Application.Products.RemoveProduct;
using Application.Products.UpdateProduct;
using Domain.Products;
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

    [HttpPut("update")]
    public async Task<IActionResult> Update(
        IServiceHandler<UpdateProductRequest, ErrorOr<Success>> service,
        UpdateProductRequest request)
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

    [HttpPatch("change-position")]
    public async Task<IActionResult> ChangePosition(
        IChangePositionService<Product> changePositionService,
        IEnumerable<ChangePositionRequest> request)
    {
        var result = await changePositionService.Handler(request);
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }
}