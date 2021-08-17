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
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private ApplicationDbContext _context;
        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<ReviewsController>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var reviews = _context.Reviews.Include(r => r.Product).Include(r => r.User).Where(review => review.ProductId == id);
            return Ok(reviews);
        }

        // GET api/<ReviewsController>/5
        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ReviewsController>
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] Review value)
        {
            _context.Reviews.Add(value);
            var product = _context.Products.FirstOrDefault(product => product.ProductId == value.ProductId);
            _context.SaveChanges();
            return Ok(value);
        }

        // PUT api/<ReviewsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Review value)
        {
            var review = _context.Reviews.FirstOrDefault(review => review.ReviewId == id);
            review.Description = value.Description;
            review.Rating = value.Rating;
            review.UserId = value.UserId;
            _context.SaveChanges();
            return Ok(review);
        }

        // DELETE api/<ReviewsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = _context.Reviews.FirstOrDefault(review => review.ReviewId == id);
            _context.Remove(review);
            _context.SaveChanges();
            return Ok();
        }
    }
}
