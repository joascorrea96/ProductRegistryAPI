using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductRegistryAPI.Data;
using ProductRegistryAPI.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ProductRegistryAPI.Queries.Suppliers
{

    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, Supplier>
    {
        private readonly AppDbContext _context;

        public GetSupplierByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Supplier> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Suppliers.Include(s => s.Address)
                                            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        }
    }
}
