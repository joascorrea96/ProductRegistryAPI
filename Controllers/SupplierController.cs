using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductRegistryAPI.Commands;
using ProductRegistryAPI.Commands.Suppliers;
using ProductRegistryAPI.Data;
using ProductRegistryAPI.Models;
using ProductRegistryAPI.Queries;
using ProductRegistryAPI.Queries.Suppliers;
using ProductRegistryAPI.Utils;
using System.Text.Json.Serialization;

namespace ProductRegistryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public SupplierController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            var suppliers = await _mediator.Send(new GetSuppliersQuery());
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplierById(int id)
        {
            var supplier = await _mediator.Send(new GetSupplierByIdQuery(id));

            if(supplier == null)
                return NotFound();

            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> CreateSupplier(CreateSupplierCommand command)
        {
            try
            {
                var supplier = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.Id }, supplier);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, UpdateSupplierCommand command)
        {
            if (id != command.Id) return BadRequest("Supplier ID mismatch");

            try
            {
                var updatedSupplier = await _mediator.Send(command);
                if (updatedSupplier == null) return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteSupplierCommand(id)); // Passe o ID para o construtor
                if (!result) return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //private bool SupplierExists(int id)
        //{
        //    return _context.Suppliers.Any(s => s.Id == id);
        //}

        //private async Task<Address> GetAddressFromCep(string cep)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
        //        if(response.IsSuccessStatusCode)
        //        {
        //            var addressJson = await response.Content.ReadAsStringAsync();
        //            var address = JsonConvert.DeserializeObject<Address>(addressJson);
        //            return address;
        //        }
        //        return null;
        //    }
        //}


    }
}
