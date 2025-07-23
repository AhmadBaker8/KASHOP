using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services
{
    public interface ICategoryService
    {
        int CreateCategory(CategoryRequest request);
        IEnumerable<CategoryResponse> GetAllCategories();
        CategoryResponse? GetCategoryById(int id);
        int UpdateCategory(int id,CategoryRequest request);
        int DeleteCategory(int id);
        bool ToggleStatus(int id);
    }
}
