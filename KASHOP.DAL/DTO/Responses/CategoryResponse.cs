using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ProductResponse>? products { get; set; }
        public Status Status { get; set; }

    }
}
