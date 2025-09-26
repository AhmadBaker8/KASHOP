using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order?> AddAsync(Order order)
        {
            return await _orderRepository.AddAsync(order);
        }
        public async Task<Order?> GetUserByOrderAsync(int orderId)
        {
            return await _orderRepository.GetUserByOrderAsync(orderId);
        }

        public async Task<bool> ChangeStatusAsync(int orederId, OrderStatus newStatus)
        {
            return await _orderRepository.ChangeStatusAsync(orederId, newStatus);
        }

        public async Task<List<Order>> GetByStatusAsync(OrderStatus orderStatus)
        {
            return await _orderRepository.GetByStatusAsync(orderStatus);
        }

        public async Task<List<Order>> GetOrderByUserAsync(string userId)
        {
            return await _orderRepository.GetOrderByUserAsync(userId);
        }

        
    }
}
