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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }






        public async Task<Order?> AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetUserByOrderAsync(int orderId)
        {
            return await _context.Orders.Include(o => o.User).FirstOrDefaultAsync(o => o.Id == orderId);
        }



        public async Task<List<Order>> GetByStatusAsync(OrderStatus orderStatus)
        {
            return await _context.Orders.Where(o => o.Status == orderStatus)
                .OrderByDescending(o => o.OrderDate).ToListAsync();
        }

        public async Task<List<Order>> GetOrderByUserAsync(string userId)
        {
            return await _context.Orders
                .Include(o => o.User).OrderByDescending(o => o.OrderDate).ToListAsync();
        }
        public async Task<bool> ChangeStatusAsync(int orederId, OrderStatus newStatus)
        {
            var order = await _context.Orders.FindAsync(orederId);
            if (order is null)
            {
                return false;
            }
            order.Status = newStatus;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserHasApprovedOrderForProductAsync(string userId, int productId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .AnyAsync(e => e.UserId == userId && e.Status == OrderStatus.Approved &&
                e.OrderItems.Any(oi => oi.ProductId == productId)); 
        }
    }
}
