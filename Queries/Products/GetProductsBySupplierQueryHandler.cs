using MediatR;
using ProductRegistryAPI.Data;
using ProductRegistryAPI.Models;

namespace ProductRegistryAPI.Queries.Products
{
    public class GetProductsBySupplierQueryHandler : IRequestHandler<GetProductsBySupplierQuery, List<Product>>
    {
        private readonly AppDbContext _context;

        public GetProductsBySupplierQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Handle(GetProductsBySupplierQuery request, CancellationToken cancellationToken)
        {
            //retorna através da procedure, todos os produtos vinculados ao id do fornecedor informado.
            return await _context.GetProductsBySupplierAsync(request.SupplierId);
        }
    }
}
