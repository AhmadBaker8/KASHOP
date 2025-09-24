using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KASHOP.DAL.Repositories.Classes
{
    public class CartRepository : ICartRepository
    {
        private ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Cart cart)
        {
            _context.Cart.AddAsync(cart);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var items = _context.Cart.Where(c => c.UserId == userId).ToList();
            _context.Cart.RemoveRange(items);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Cart>> GetUserCartAsync(string userId)
        {
            return await _context.Cart.Include(c=>c.Product).Where(c => c.UserId == userId).ToListAsync();
        }
    }
}
