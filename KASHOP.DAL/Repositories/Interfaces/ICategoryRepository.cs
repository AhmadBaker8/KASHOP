using KASHOP.DAL.Models;

namespace KASHOP.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category?> GetByIdAsync(int id);


    }
}