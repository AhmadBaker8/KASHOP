using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<bool> AddReviewAsync(ReviewRequest reviewRequest, string userId)
        {
            var hasOrder = _orderRepository.UserHasApprovedOrderForProductAsync(userId, reviewRequest.ProductId);
            if (!hasOrder.Result)
            {
                return false;
            }
            var alreadyReviewed = await _reviewRepository.HasUserReviewedProduct(userId, reviewRequest.ProductId);
            if (alreadyReviewed)
            {
                return false;
            }
            var review = reviewRequest.Adapt<Review>();
            await _reviewRepository.AddReviewAsync(review,userId);
            return true;

        }
    }
}
