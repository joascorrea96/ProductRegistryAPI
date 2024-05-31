using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductRegistryAPI.Data;
using ProductRegistryAPI.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace ProductRegistryAPI.Queries.Suppliers
{
    public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, List<Supplier>>
    {
        private readonly AppDbContext _context;

        public GetSuppliersQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Supplier>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Suppliers.Include(s => s.Address).ToListAsync(cancellationToken);
        }
    }

}
