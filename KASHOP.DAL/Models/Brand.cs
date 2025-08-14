using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Models
{
    public class Brand : BaseModel
    {
        public string Name { get; set; }

        public List<Product> products { get; set; } = new List<Product>();
    }
}
