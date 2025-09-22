using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class CheckOutService : ICheckOutService
    {
        private readonly ICartRepository _cartRepository;
        public CheckOutService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Task<CheckOutResponse> ProccessPaymentAsync(CheckOutRequest request, string userId, HttpRequest Request)
        {
            var cartItems = _cartRepository.GetUserCart(userId);
            if (!cartItems.Any())
            {
                return Task.FromResult(new CheckOutResponse
                {
                    Success = false,
                    Message = "Cart is empty"
                });
            }


            Order order = new Order
            {
                UserId = userId,
                PaymentMethod = PaymentMethodEnum.Cash,
                TotalAmount = cartItems.Sum(c => c.Product.Price * c.Count),
            };


            if (request.PaymentMethod == PaymentMethodEnum.Cash) 
            {
                return Task.FromResult(new CheckOutResponse
                {
                    Success = true,
                    Message = "Order placed successfully. Pay on delivery."
                });
            }

            if (request.PaymentMethod == PaymentMethodEnum.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {

                    },
                    Mode = "payment",
                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/api/Customer/CheckOuts/success/{order.Id}",
                    CancelUrl = $"{Request.Scheme}://{Request.Host}/api/Customer/CheckOuts/cancel",
                };
                foreach (var item in cartItems)
                {

                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                            },
                            UnitAmount = (long)(item.Product.Price),
                        },
                        Quantity = item.Count,
                    });
                }
                var service = new SessionService();
                var session = service.Create(options);
                order.PaymentId = session.Id;
                return Task.FromResult(new CheckOutResponse
                {
                    Success = true,
                    Message = "Payment processed successfully",
                    PaymentId = session.Id,
                    Url = session.Url
                });
            }
            return Task.FromResult(new CheckOutResponse
            {
                Success = false,
                Message = "Invalid payment method"
            });
        }



        public Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
