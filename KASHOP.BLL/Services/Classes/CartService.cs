using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class CartService : ICartService
    {
        private ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
           _cartRepository = cartRepository;
        }
        public bool AddToCart(CartRequest request, string userId)
        {
            var newItem = new Cart
            {
                ProductId = request.ProductId,
                UserId = userId,
                Count = 1
            };
            return _cartRepository.Add(newItem) > 0;
        }

        public CartSummaryResponse CartSummaryResponse(string userId)
        {
            var cartItems = _cartRepository.GetUserCart(userId);
            var response = new CartSummaryResponse
            {
                Items = cartItems.Select(ci => new CartResponse
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Quantity = ci.Count,
                    Price = ci.Product.Price,
                }).ToList()
            };
            return response;
        }
    }
}
