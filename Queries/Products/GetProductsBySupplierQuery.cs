using MediatR;
using ProductRegistryAPI.Models;

namespace ProductRegistryAPI.Queries.Products
{
    public class GetProductsBySupplierQuery : IRequest<List<Product>>
    {
        public int SupplierId { get; set; }
    }
}
