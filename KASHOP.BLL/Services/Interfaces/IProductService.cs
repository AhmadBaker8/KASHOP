using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface IProductService : IGenericService<ProductRequest,ProductResponse,Product>
    {
        Task<int> CreateFile(ProductRequest request);
    }
}
