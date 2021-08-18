using eCommerceStarterCode.Data;
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
            var product = _context.Products.Include(p => p.User).Include(p => p.Category).FirstOrDefault(product => product.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            var reviews = _context.Reviews.Where(review => review.ProductId == id);
            var total = 0;
            foreach (var review in reviews)
            {
                total += review.Rating;
            }
            if (reviews.Count() != 0)
            {
                int averageRating = total / reviews.Count();
                product.AverageRating = averageRating;
                _context.SaveChanges();
            }
            return Ok(product); 
        }

        [HttpGet("userProducts/{id}")]

        public IActionResult Get(string id)
        {
            var userProducts = _context.Products.Where(product => product.UserId == id);
            return Ok(userProducts);
        }

        // POST api/product
        [HttpPost, Authorize]
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
            product.Image = value.Image;
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
