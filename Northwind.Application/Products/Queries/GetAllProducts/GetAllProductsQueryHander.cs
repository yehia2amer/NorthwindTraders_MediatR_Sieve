using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Persistence;
using Sieve.Services;

namespace Northwind.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductsListViewModel>
    {
        private readonly NorthwindDbContext _context;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(NorthwindDbContext context, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _context = context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<ProductsListViewModel> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            // TODO: Set view model state based on user permissions.
            //List<Domain.Entities.Product> products = await _context.Products.OrderBy(p => p.ProductName).Include(p => p.Supplier).ToListAsync(cancellationToken);
            var products = _context.Products.AsNoTracking().OrderBy(p => p.ProductName).Include(p => p.Supplier);

            var Products_Sorted_Filtered_Paginated = _sieveProcessor.Apply(request.SieveModel, products); // Returns `result` after applying the sort/filter/page query in `SieveModel` to it 
            var model = new ProductsListViewModel
            {
                Products = _mapper.Map<IEnumerable<ProductDto>>(await Products_Sorted_Filtered_Paginated.ToListAsync(cancellationToken)),
                CreateEnabled = true
            };

            return model;
        }
    }
}
