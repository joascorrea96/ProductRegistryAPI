using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductRegistryAPI.Commands.Products;
using ProductRegistryAPI.Models;
using ProductRegistryAPI.Queries.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRegistryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("bysupplier/{supplierId}")]
        public async Task<ActionResult<List<Product>>> GetProductsBySupplier(int supplierId)
        {
            var products = await _mediator.Send(new GetProductsBySupplierQuery { SupplierId = supplierId });
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductCommand command)
        {
            try
            {
                var product = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommand command)
        {
            if (id != command.Id) return BadRequest("ID não encontrado");

            try
            {
                var updatedProduct = await _mediator.Send(command);
                if (updatedProduct == null) return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteProductCommand(id));
                if (!result) return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
