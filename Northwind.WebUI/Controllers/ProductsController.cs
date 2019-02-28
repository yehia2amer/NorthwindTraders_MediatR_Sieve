﻿using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Products.Commands.CreateProduct;
using Northwind.Application.Products.Commands.DeleteProduct;
using Northwind.Application.Products.Commands.UpdateProduct;
using Northwind.Application.Products.Queries.GetAllProducts;
using Northwind.Application.Products.Queries.GetProduct;
using Microsoft.AspNetCore.Http;
using Sieve.Models;

namespace Northwind.WebUI.Controllers
{
    public class ProductsController : BaseController
    {
        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<ProductsListViewModel>> GetAll([FromQuery] SieveModel sieveModel)
        {
            GetAllProductsQuery request = new GetAllProductsQuery(sieveModel);
            return base.Ok(await Mediator.Send(request));


            //return Ok(await Mediator.Send(new GetAllProductsQuery(sieveModel)));
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetProductQuery { Id = id }));
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
        {
            var productId = await Mediator.Send(command);

            return Ok(productId);
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Update(
            [FromRoute] int id,
            [FromBody] UpdateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProductCommand { Id = id });

            return NoContent();
        }
    }
}