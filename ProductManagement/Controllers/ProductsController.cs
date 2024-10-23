using System.Security.Cryptography;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;
        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var resultObject = await mediator.Send(new GetProductsQuery());
            return resultObject.Match<IActionResult>(
                onSuccess: result => Ok(result),
                onFailure: error => BadRequest(error)
            );
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            var resultObject = await mediator.Send(new GetProductByIdQuery{Id = id});
            return resultObject.Match<IActionResult>(
                onSuccess: result => Ok(result),
                onFailure: error => BadRequest(error)
            );
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand createProductCommand)
        {
            var resultObject = await mediator.Send(createProductCommand);
            return resultObject.Match<IActionResult>(
                onSuccess: result => Ok(result),
                onFailure: error => BadRequest(error)
            );
        }
        
        [HttpPut("id")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductCommand command)
        {
            if(id != command.Id)
            {
                return BadRequest("Id's dont match.");
            }
            var resultObject = await mediator.Send(command);
            return resultObject.Match<IActionResult>(
                onSuccess: () => NoContent(),
                onFailure: error => BadRequest(error)
            );
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var resultObject = await mediator.Send(new DeleteProductCommand { Id = id });
            return resultObject.Match<IActionResult>(
                onSuccess: () => NoContent(),
                onFailure: error => BadRequest(error)
            );
        }
    }
}
