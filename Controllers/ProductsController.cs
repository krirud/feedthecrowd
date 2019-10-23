using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FeedTheCrowd.Models;
using FeedTheCrowd.Services.Interfaces;
using FeedTheCrowd.Dtos.Products;
using Microsoft.AspNetCore.Authorization;

namespace FeedTheCrowd.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : Controller
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        // GET: api/Products/5                                                                            
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("{id}/recipe")]
        public async Task<ActionResult<Product>> GetProductsForRecipe(int id)
        {
            var products = await _productService.GetByRecipeId(id);
            return Ok(products);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NewProductDto[] newProducts)
        {
            await _productService.Update(id, newProducts);
            return NoContent();
        }

    }
}
