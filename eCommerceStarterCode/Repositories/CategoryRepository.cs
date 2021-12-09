using eCommerceStarterCode.Data;
using eCommerceStarterCode.Interfaces;
using eCommerceStarterCode.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await this._context.Categories.ToListAsync();
        }
    }
}
