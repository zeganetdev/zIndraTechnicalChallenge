using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.CreateProduct;
using zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.DeleteProduct;
using zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.GetProduct;
using zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.UpdateProduct;
using zIndraTechnicalChallenge.Application.MainContext.Features.Products.Queries.GetListProduct;

namespace zIndraTechnicalChallenge.Service.Rest.MainContext.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{

    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;

    public ProductController(ILogger<ProductController> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<ActionResult> CreateProductAsync([FromBody] CreateProductCommand createProductCommand, CancellationToken cancellationToken)
    {
        _logger.LogInformation("init CreateProductAsync");
        var data = await _mediator.Send(createProductCommand, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, data);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> DeleteProductAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("init DeleteProductAsync");
        await _mediator.Send(new DeleteProductCommand { Id = id }, cancellationToken);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProductAsync([FromBody] UpdateProductCommand updateProductCommand, CancellationToken cancellationToken)
    {
        _logger.LogInformation("init UpdateProductAsync");
        var data = await _mediator.Send(updateProductCommand, cancellationToken);
        return StatusCode(StatusCodes.Status200OK, data);
    }

    [HttpGet]
    public async Task<ActionResult> GetListProductAsync([FromQuery] GetListProductCommand getListProductCommand, CancellationToken cancellationToken)
    {
        _logger.LogInformation("init GetListProductAsync");
        var data = await _mediator.Send(getListProductCommand, cancellationToken);
        return StatusCode(StatusCodes.Status200OK, data);
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult> GetProductAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("init GetProductAsync");
        var data = await _mediator.Send(new GetProductCommand { Id = id }, cancellationToken);
        return StatusCode(StatusCodes.Status200OK, data);
    }

}
