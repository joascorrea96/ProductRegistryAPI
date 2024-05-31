using MediatR;
using ProductRegistryAPI.Models;
using System.Collections.Generic;

namespace ProductRegistryAPI.Queries.Products
{
    public record GetProductsQuery : IRequest<IEnumerable<Product>>;
}
