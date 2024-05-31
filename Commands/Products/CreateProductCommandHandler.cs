using MediatR;
using ProductRegistryAPI.Data;
using ProductRegistryAPI.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductRegistryAPI.Commands.Products
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly AppDbContext _context;

        public CreateProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Description = request.Description,
                Brand = request.Brand,
                UnitOfMeasure = request.UnitOfMeasure,
                PhotoUrl = request.PhotoUrl,
                SupplierId = request.SupplierId
            };

            if (_context.Products.Any(p => p.Description == product.Description && p.Brand == product.Brand))
            {
                throw new InvalidOperationException("Produto com esta descrição e marca já existe.");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return product;
        }
    }
}
