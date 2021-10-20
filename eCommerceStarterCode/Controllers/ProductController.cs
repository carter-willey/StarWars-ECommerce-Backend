using eCommerceStarterCode.Data;
using eCommerceStarterCode.Interfaces;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerceStarterCode.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        // GET: api/<ProductController>
        //GETS ALL PRODUCTS 
        [HttpGet]
        public async Task<List<Product>> GetAllProducts()
        {
            return await this._productRepository.GetAllProducts();
        }

        //GET api/product/5
        [HttpGet("{productId}")]
        public async Task<List<Product>> GetProduct(int productId)
        {
            return await this._productRepository.GetProduct(productId);

        }

        [HttpGet("userProducts/{userId}")]

        public async Task<List<Product>> GetUsersProducts(string userId)
        {
            return await this._productRepository.GetUsersProducts(userId);
        }

        // POST api/product
        [HttpPost, Authorize]
        public async Task <ActionResult> AddNewProduct([FromBody] Product product)
        {
            if(ModelState.IsValid)
            {
                bool isValid = await this._productRepository.AddNewProduct(product);
                if (isValid)
                {
                    return Ok();
                }
            }
            return BadRequest();

        }

        //// PUT api/<ProductController>/5
        [HttpPut("{productId}")]
        public async Task<ActionResult> UpdateUserProduct(int productId, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                bool isValid = await this._productRepository.UpdateUserProduct(productId, product);
                if (isValid)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        //// DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserProduct(int productId)
        {
            bool isValid = await this._productRepository.DeleteUserProduct(productId);
            if (isValid)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
