using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Mvc;
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
        private ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<ProductController>
        //GETS ALL PRODUCTS 
        [HttpGet]
        public IActionResult Get()
        {
            
            var products = _context.Products;
            return Ok(products);
        }

        //GET api/product/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.FirstOrDefault(product => product.ProductId == id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product); 
        }

        // POST api/product
        [HttpPost]
        public IActionResult Post([FromBody]Product value)
        {
            _context.Products.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        //// PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product value)
        {
            var product = _context.Products.FirstOrDefault(product => product.ProductId == id);
            product.Name = value.Name;
            product.Price = value.Price;
            product.Description = value.Description;
            product.AverageRating = value.AverageRating;
            product.CategoryId = value.CategoryId;
            product.UserId = value.UserId;
            product.ReviewId = value.ReviewId;
            _context.SaveChanges();
            return Ok(product);
        }

        //// DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(product => product.ProductId == id);
            _context.Remove(product);
            _context.SaveChanges();
            return Ok();

        }
    }
}
