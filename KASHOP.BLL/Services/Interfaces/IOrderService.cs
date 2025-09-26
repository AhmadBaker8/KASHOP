using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetUserByOrderAsync(int orderId);
        Task<Order?> AddAsync(Order order);
        Task<List<Order>> GetByStatusAsync(OrderStatus orderStatus);
        Task<List<Order>> GetOrderByUserAsync(string userId);
        Task<bool> ChangeStatusAsync(int orederId, OrderStatus newStatus);

    }
}
