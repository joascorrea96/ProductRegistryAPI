using MediatR;
using ProductRegistryAPI.Models;

namespace ProductRegistryAPI.Commands.Products
{
    public record UpdateProductCommand(int Id, string Description, string Brand, UnitOfMeasure UnitOfMeasure, string PhotoUrl) : IRequest<Product>;
}
