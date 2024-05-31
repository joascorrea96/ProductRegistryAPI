using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductRegistryAPI.Data;
using ProductRegistryAPI.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductRegistryAPI.Queries.Products
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly AppDbContext _context;

        public GetProductsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }
    }
}
