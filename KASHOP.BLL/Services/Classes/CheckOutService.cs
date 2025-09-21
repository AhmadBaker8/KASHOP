using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
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
                return new Task<CheckOutResponse>(() => new CheckOutResponse
                {
                    Success = false,
                    Message = "Cart is empty"
                });
            }

            if (request.PaymentMethod == "Cash") 
            {
                return new Task<CheckOutResponse>(() => new CheckOutResponse
                {
                    Success = true,
                    Message = "Order placed successfully. Pay on delivery."
                });
            }

            if (request.PaymentMethod == "Visa")
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {

                    },
                    Mode = "payment",
                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                    CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
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
                return new Task<CheckOutResponse>(() => new CheckOutResponse
                {
                    Success = true,
                    Message = "Payment processed successfully",
                    //SessionId = session.Id
                    Url = session.Url
                });
            }
            return new Task<CheckOutResponse>(() => new CheckOutResponse
            {
                Success = false,
                Message = "Invalid payment method"
            });
        }
    }
}
