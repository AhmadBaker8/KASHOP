using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task DecreaseQuantityAsync(List<(int productId,int qunatity)> items);

        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<Product?> GetByIdAsync(int id);
    }
}
