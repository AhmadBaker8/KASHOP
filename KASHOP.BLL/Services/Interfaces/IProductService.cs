using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface IProductService : IGenericService<ProductRequest,ProductResponse,Product>
    {
        Task<int> CreateProduct(ProductRequest request);

        Task<List<ProductResponse>> GetAllProducts(HttpRequest httpRequest, bool onlyActive = false);
    }
}
