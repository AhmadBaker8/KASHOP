using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;
        private readonly IOrderItemRepository _orderItemRepository;
        public CheckOutService(ICartRepository cartRepository, IOrderRepository orderRepository, IEmailSender emailSender,IOrderItemRepository orderItemRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<CheckOutResponse> ProccessPaymentAsync(CheckOutRequest request, string userId, HttpRequest Request)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(userId);
            if (!cartItems.Any())
            {
                return new CheckOutResponse
                {
                    Success = false,
                    Message = "Cart is empty"
                };
            }


            Order order = new Order
            {
                UserId = userId,
                PaymentMethod = request.PaymentMethod,
                TotalAmount = cartItems.Sum(c => c.Product.Price * c.Count),
            };
            await _orderRepository.AddAsync(order);

            if (request.PaymentMethod == PaymentMethodEnum.Cash) 
            {
                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Order placed successfully. Pay on delivery."
                };
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
                return new CheckOutResponse
                {
                    Success = true,
                    Message = "Payment processed successfully",
                    PaymentId = session.Id,
                    Url = session.Url
                };
            }
            return new CheckOutResponse
            {
                Success = false,
                Message = "Invalid payment method"
            };
        }



        public async Task<bool> HandlePaymentSuccessAsync(int orderId)
        {
            var order = await _orderRepository.GetUserByOrderAsync(orderId);

            var subject = "";
            var body = "";
            if (order.PaymentMethod == PaymentMethodEnum.Visa)
            {

                order.Status = OrderStatus.Approved;

                var carts = await _cartRepository.GetUserCartAsync(order.UserId);
                var orderItems = new List<OrderItems>();
                foreach (var item in carts)
                {
                    var orderItem = new OrderItems
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Count,
                        Price = item.Product.Price,
                        TotalPrice = (double)item.Product.Price * item.Count,

                    };

                    orderItems.Add(orderItem);
                }
                await _orderItemRepository.AddRangeAsync(orderItems);







                subject = "Payment Successful";
                body = $"<h1>Thank you for your payment </h1> " + 
                    $"<p>Your payment for order {orderId}</p>" + 
                    $"<p>Total Amount : {order.TotalAmount}</p>";
                return true;
            }
            else if(order.PaymentMethod == PaymentMethodEnum.Cash)
            {
                subject = "Order Placed Successfully";
                body = $"<h1>Thank you for your order </h1> " +
                    $"<p>Your order {orderId}</p>" +
                    $"<p>Total Amount : {order.TotalAmount}</p>" +
                    $"<p>Please pay on delivery</p>";
                order.Status = OrderStatus.Approved;
                return true;
            }

            await _emailSender.SendEmailAsync(order.User.Email, subject, body);
            return true;

        }
    }
}
