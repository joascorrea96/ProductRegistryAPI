using MediatR;
using ProductRegistryAPI.Models;
using System.Collections.Generic;

namespace ProductRegistryAPI.Queries.Suppliers
{
    public record GetSuppliersQuery : IRequest<List<Supplier>>;

}
