using MediatR;

namespace ProductRegistryAPI.Commands.Suppliers
{
    public record DeleteSupplierCommand(int Id) : IRequest<bool>;

}
