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
    public interface ICategoryService : IGenericService<CategoryRequest, CategoryResponse, Category>
    {
        Task<List<CategoryResponse>> GetAllCategories();
        Task<CategoryResponse> GetCategoryById(int id);
        Task<int> CreateCategory(CategoryRequest request);
        Task<bool> UpdateCategory(int id, CategoryRequest request);
        Task<bool> DeleteCategory(int id);
        Task<bool> ToggleStatus(int id);

    }
}
