using MediatR;
using ProductRegistryAPI.Models;

namespace ProductRegistryAPI.Queries.Products
{
    public record GetProductByIdQuery(int Id) : IRequest<Product>;
}
