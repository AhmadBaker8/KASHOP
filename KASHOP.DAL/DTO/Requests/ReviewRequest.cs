using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.DTO.Requests
{
    public class ReviewRequest
    {
        public int ProductId { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
    }
}
