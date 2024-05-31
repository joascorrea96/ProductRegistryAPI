using MediatR;
using ProductRegistryAPI.Data;
using ProductRegistryAPI.Models;
using ProductRegistryAPI.Utils;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ProductRegistryAPI.Commands.Suppliers
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Supplier>
    {
        private readonly AppDbContext _context;

        public CreateSupplierCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Supplier> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
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

            var supplier = new Supplier
            {
                Name = request.Name,
                CNPJ = formattedCNPJ,
                Address = address,
                Phone = request.Phone
            };

            if (_context.Suppliers.Any(s => s.CNPJ == supplier.CNPJ))
            {
                throw new InvalidOperationException("Fornecedor com este CNPJ já existe.");
            }

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync(cancellationToken);

            return supplier;
        }

    }

}
