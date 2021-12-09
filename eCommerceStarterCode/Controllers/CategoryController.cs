using eCommerceStarterCode.Data;
using eCommerceStarterCode.Interfaces;
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

        private ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository) // Passed in new instance to the constructor and set _categoryRepository equal to this new instance
        {
            this._categoryRepository = categoryRepository;
        }

        // GET: api/<CategoryController>
        // Gets all categories from the database
        [HttpGet]
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await this._categoryRepository.GetAllCategoriesAsync();
        }
    }
}
