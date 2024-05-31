using MediatR;
using ProductRegistryAPI.Models;

namespace ProductRegistryAPI.Queries.Suppliers
{

    public record GetSupplierByIdQuery(int Id) : IRequest<Supplier>;

}
