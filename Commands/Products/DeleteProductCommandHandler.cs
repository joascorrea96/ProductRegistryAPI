using MediatR;
using ProductRegistryAPI.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ProductRegistryAPI.Commands.Products
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
