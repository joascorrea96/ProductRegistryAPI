using MediatR;
using ProductRegistryAPI.Data;
using ProductRegistryAPI.Models;
using ProductRegistryAPI.Utils;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProductRegistryAPI.Commands.Suppliers
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Supplier>
    {
        private readonly AppDbContext _context;

        public UpdateSupplierCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Supplier> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.FindAsync(request.Id);

            if (supplier == null) return null;

            var formattedCNPJ = SupplierUtils.FormatCNPJ(request.CNPJ);

            if (!SupplierUtils.IsValidCNPJ(formattedCNPJ))
            {
                throw new ArgumentException("CNPJ inválido.");
            }

            var address = await SupplierUtils.GetAddressFromCep(request.AddressCep.Cep);
            if (address == null)
            {
                throw new ArgumentException("CEP inválido.");
            }

            supplier.Address = address;
            supplier.Name = request.Name;
            supplier.Phone = request.Phone;

            await _context.SaveChangesAsync(cancellationToken);

            return supplier;
        }


    }

}
