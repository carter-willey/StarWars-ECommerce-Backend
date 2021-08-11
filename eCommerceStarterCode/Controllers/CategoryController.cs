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
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<CategoryController>
        // GET ALL CATEGORIES
        [HttpGet]
        public IActionResult Get()
        {
            var categories = _context.Categories;
            return Ok(categories);
        }

        // GET api/<CategoryController>/{id}
        // GET CATEGORY BY ID
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _context.Categories.FirstOrDefault(category => category.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST api/<CategoryController>
        // CREATE A CATEGORY
        [HttpPost]
        public IActionResult Post([FromBody] Category value)
        {
            _context.Categories.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        // PUT api/<CategoryController>/{id}
        // PUT CATEGORY
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category value)
        {
            var category = _context.Categories.FirstOrDefault(category => category.CategoryId == id);
            category.CategoryName = value.CategoryName;
            _context.SaveChanges();
            return Ok(category);
        }

        // DELETE api/<CategoryController>/{id}
        // DELETE CATEGORY BY ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(category => category.CategoryId == id);
            _context.Remove(category);
            _context.SaveChanges();
            return Ok();
        }
    }
}
