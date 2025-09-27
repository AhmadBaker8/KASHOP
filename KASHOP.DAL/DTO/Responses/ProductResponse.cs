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
        
        public int Quantity { get; set; }
        [JsonIgnore]
        public string MainImage { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> SubImagesUrls { get; set; } = new List<string>();

    }
}
