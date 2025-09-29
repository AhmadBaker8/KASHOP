using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; } = Status.Active;
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
