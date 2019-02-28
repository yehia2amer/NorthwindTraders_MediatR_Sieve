using Northwind.Domain.Entities;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Persistence.Configurations
{
        public class ApplicationSieveProcessor : SieveProcessor
        {
            public ApplicationSieveProcessor(
                Microsoft.Extensions.Options.IOptions<SieveOptions> options,
                ISieveCustomSortMethods customSortMethods,
                ISieveCustomFilterMethods customFilterMethods)
                : base(options, customSortMethods, customFilterMethods)
            {
            }

            protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
            {
                mapper.Property<Product>(e => e.ProductId).CanSort().CanFilter();
                mapper.Property<Product>(e => e.ProductName).CanSort().CanFilter();

                return mapper;
            }
        }
}
