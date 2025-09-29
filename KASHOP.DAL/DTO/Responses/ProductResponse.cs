using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace KASHOP.DAL.DTO.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountedPrice => Price - (Price * Discount / 100);
        public int Quantity { get; set; }
        [JsonIgnore]
        public string MainImage { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> SubImagesUrls { get; set; } = new List<string>();
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int TotalReviews => Reviews?.Count ?? 0;
        public List<ReviewResponse> Reviews { get; set; } = new List<ReviewResponse>();

        public DateTime CreatedAt { get; set; }
        


    }
}
