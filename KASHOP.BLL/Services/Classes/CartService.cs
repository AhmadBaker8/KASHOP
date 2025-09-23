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
        public async Task<bool> AddToCartAsync(CartRequest request, string userId)
        {
            var newItem = new Cart
            {
                ProductId = request.ProductId,
                UserId = userId,
                Count = 1
            };
            return await _cartRepository.AddAsync(newItem) > 0;
        }

        public async Task<CartSummaryResponse> CartSummaryResponseAsync(string userId)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(userId);
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
