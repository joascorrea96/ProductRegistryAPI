using MediatR;
using ProductRegistryAPI.Models;

namespace ProductRegistryAPI.Commands.Products
{
    public record CreateProductCommand(string Description, string Brand, UnitOfMeasure UnitOfMeasure, string PhotoUrl, int SupplierId) : IRequest<Product>;
}
