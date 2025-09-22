using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Responses
{
    public class BrandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string Image { get; set; }
        public string ImageUrl => $"https://localhost:7156/images/{Image}";

    }
}
