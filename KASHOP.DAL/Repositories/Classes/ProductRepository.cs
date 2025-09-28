using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Classes
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DecreaseQuantityAsync(List<(int productId, int qunatity)> items)
        {
            //var product = await _context.Products.FindAsync(productId);
            //if (product is null)
            //{
            //    throw new Exception("Product not found");
            //}
            //if(product.Quantity < quantity)
            //{
            //    throw new Exception("Insufficient product quantity");
            //}
            //product.Quantity -= quantity;
            //await _context.SaveChangesAsync();
            var productIds = items.Select(i => i.productId).ToList();

            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
            foreach (var product in products)
            {
                var item = items.First(i => i.productId == product.Id);
                if (product is null)
                {
                    throw new Exception($"Product with ID {item.productId} not found");
                }
                if (product.Quantity < item.qunatity)
                {
                    throw new Exception($"Insufficient quantity for product ID {item.productId}");
                }
                product.Quantity -= item.qunatity;
            }
            await _context.SaveChangesAsync();

        }

        public List<Product> GetAllProductsWithImages()
        {
            return _context.Products
                .Include(p => p.SubImages)
                .Include(p => p.Reviews)
                .ThenInclude(r => r.User)
                .ToList();
        }
    }
}
