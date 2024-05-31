using MediatR;
using ProductRegistryAPI.Models;

namespace ProductRegistryAPI.Commands.Suppliers
{
    public record CreateSupplierCommand(string Name, string CNPJ, Address AddressCep, string Phone) : IRequest<Supplier>;

}
