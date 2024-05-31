using MediatR;
using ProductRegistryAPI.Data;
using ProductRegistryAPI.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductRegistryAPI.Commands.Products
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly AppDbContext _context;

        public UpdateProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null)
            {
                return null;
            }

            product.Description = request.Description;
            product.Brand = request.Brand;
            product.UnitOfMeasure = request.UnitOfMeasure;
            product.PhotoUrl = request.PhotoUrl;

            await _context.SaveChangesAsync(cancellationToken);

            return product;
        }
    }
}
