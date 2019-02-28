using MediatR;
using Sieve.Models;

namespace Northwind.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<ProductsListViewModel>
    {
        public SieveModel SieveModel { get; }

        public GetAllProductsQuery(SieveModel sieveModel) => SieveModel = sieveModel;
    }
}