using Application.Use_Cases.Commands;
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
<<<<<<< HEAD
            return Ok("Creare de produs");
=======
            return Ok("Obtinere de produse");
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            return Ok($"Obtinerea produsului cu id: {id}");
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateProduct()
        {
            return Ok("Creare de produs");
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(Guid id)
        {
            return Ok($"Actualizare pentru produsul cu id: {id}");
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            return Ok($"Stergerea produsului cu id: {id}");
>>>>>>> 25eb6ebdb4b476c199a417b6140c691d2fe13753
        }
    }
}
