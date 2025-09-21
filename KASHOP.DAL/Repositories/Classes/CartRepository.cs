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
        public int Add(Cart cart)
        {
            _context.Cart.Add(cart);
            return _context.SaveChanges();
        }

        public List<Cart> GetUserCart(string userId)
        {
            return _context.Cart.Include(c=>c.Product).Where(c => c.UserId == userId).ToList();
        }
    }
}
