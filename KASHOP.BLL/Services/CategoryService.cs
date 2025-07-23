using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository CategoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.CategoryRepository = categoryRepository;
        }
        public int CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            return CategoryRepository.Add(category);
        }
        public int DeleteCategory(int id)
        {
            var category = CategoryRepository.GetById(id);
            if (category is null) return 0;
            return CategoryRepository.Remove(category);
        }

        public IEnumerable<CategoryResponse> GetAllCategories()
        {
            var categories = CategoryRepository.GetAll();
            return categories.Adapt<IEnumerable<CategoryResponse>>();
        }

        public CategoryResponse? GetCategoryById(int id)
        {
            var category = CategoryRepository.GetById(id);
            return category is null ? null : category.Adapt<CategoryResponse>();
        }

        public int UpdateCategory(int id, CategoryRequest request)
        {
            var category = CategoryRepository.GetById(id);
            if (category is null) return 0;
            category.Name = request.Name;
            return CategoryRepository.Update(category);
        }
        public bool ToggleStatus(int id)
        {
            var categoy = CategoryRepository.GetById(id);
            if(categoy is null) return false;
            categoy.Status = categoy.Status == Status.Active ? Status.Inactive : Status.Active;
            CategoryRepository.Update(categoy);
            return true;
        }
    }
}
