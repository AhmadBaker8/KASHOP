using KASHOP.DAL.Models;

namespace KASHOP.DAL.Repositories
{
    public interface ICategoryRepository
    {

        int Add(Category category);
        IEnumerable<Category> GetAll(bool withTracking = false);
        Category? GetById(int id);

        int Remove(Category category);
        int Update(Category category);
    }
}