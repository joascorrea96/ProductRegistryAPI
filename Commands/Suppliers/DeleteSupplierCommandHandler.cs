using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductRegistryAPI.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductRegistryAPI.Commands.Suppliers
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteSupplierCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.Include(s => s.Products).FirstOrDefaultAsync(s => s.Id == request.Id);

            if (supplier == null) return false;

            _context.Suppliers.Remove(supplier);
            foreach (var item in supplier.Products)
            {
                _context.Products.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    public class DeleteSupplierResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

}
