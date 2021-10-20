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
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> CheckIfProductExists(int productId)
        {
            return await this._context.Products.FindAsync(productId) != null;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await this._context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetProduct(int productId)
        {
           var product = await this._context.Products.Include(p => p.User).Include(p => p.Category).Where(product => product.ProductId == productId).ToListAsync();
           if(product != null)
           {
             await UpdateProductReviews(productId);
           }
            return product;
        }

        public async Task UpdateProductReviews(int productId)
        {
            var product = this._context.Products.FirstOrDefault(p => p.ProductId == productId);
            var reviews = this._context.Reviews.Where(review => review.ProductId == productId);
            var total = 0;
            foreach (var review in reviews)
            {
                total += review.Rating;
            }
            if (reviews.Count() != 0)
            {
                int averageRating = total / reviews.Count();
                product.AverageRating = averageRating;
                await this._context.SaveChangesAsync();
            }

        }

        public async Task<List<Product>> GetUsersProducts(string userId)
        {
            return await this._context.Products.Where(product => product.UserId == userId).ToListAsync();
        }

        public async Task<bool> AddNewProduct(Product product)
        {
            bool productExists = await CheckIfProductExists(product.ProductId);
            if (!productExists)
            {
               this._context.Products.Add(product);
               await  this._context.SaveChangesAsync();
               return true;
            }

            return false;

        }
        public async Task<bool> UpdateUserProduct(int productId, Product product)
        {
            var productToUpdate = _context.Products.FirstOrDefault(product => product.ProductId == productId);
            if(productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.Description = product.Description;
                productToUpdate.AverageRating = product.AverageRating;
                productToUpdate.Image = product.Image;
                await this._context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(product => product.ProductId == productId);
            if(product != null)
            {
                this._context.Products.Remove(product);
                await this._context.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
