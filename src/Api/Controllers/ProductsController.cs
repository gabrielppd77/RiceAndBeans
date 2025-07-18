using Api.Controllers.Common;
using Application.Common.ServiceHandler;
using Application.Products.CreateProduct;
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
}