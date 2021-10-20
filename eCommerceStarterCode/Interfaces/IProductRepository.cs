using eCommerceStarterCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetProduct(int productId);
        Task<List<Product>> GetUsersProducts(string userId);
        Task<bool> AddNewProduct(Product product);
        Task<bool> UpdateUserProduct(int productId, Product product);
        Task<bool> DeleteUserProduct(int productId);

    }
}
