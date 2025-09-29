using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class CategoryService : GenericService<CategoryRequest, CategoryResponse, Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
        {
           _categoryRepository = categoryRepository;
        }

        public async Task<int> CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            category.CreatedAt = DateTime.UtcNow;
            return _categoryRepository.Add(category);
        }

        public Task<bool> DeleteCategory(int id)
        {
            var existingCategory = _categoryRepository.GetById(id);
            if (existingCategory == null)
            {
                return Task.FromResult(false);
            }
            _categoryRepository.Remove(existingCategory);
            return Task.FromResult(true);
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            return categories.Adapt<List<CategoryResponse>>();
        }

        public async Task<CategoryResponse> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category.Adapt<CategoryResponse>();
        }

        public Task<bool> UpdateCategory(int id, CategoryRequest request)
        {
            var existingCategory = _categoryRepository.GetById(id);
            if(existingCategory == null)
            {
                return Task.FromResult(false);
            }
            var updatedCategory = request.Adapt(existingCategory);
            _categoryRepository.Update(updatedCategory);
            return Task.FromResult(true);
        }
        public Task<bool> ToggleStatus(int id)
        {
            var existingCategory = _categoryRepository.GetById(id);
            if (existingCategory == null)
            {
                return Task.FromResult(false);
            }
            existingCategory.Status = existingCategory.Status == Status.Active ? Status.Inactive : Status.Active;
            _categoryRepository.Update(existingCategory);
            return Task.FromResult(true);
        }

    }
}
