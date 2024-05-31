using MediatR;

namespace ProductRegistryAPI.Commands.Products
{
    public record DeleteProductCommand(int Id) : IRequest<bool>;
}
