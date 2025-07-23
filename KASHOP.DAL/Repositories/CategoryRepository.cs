using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryRepository(ApplicationDbContext context)
        {
            dbContext = context;

        }

        public int Add(Category category)
        {
            dbContext.Categories.Add(category);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Category> GetAll(bool withTracking)
        {
            if(withTracking)
            return dbContext.Categories.ToList();
            return dbContext.Categories.AsNoTracking().ToList();
        }

        public Category? GetById(int id)=>dbContext.Categories.Find(id);
        

        public int Remove(Category category)
        {
            dbContext.Categories.Remove(category);
            return dbContext.SaveChanges();
        }

        public int Update(Category category)
        {
            dbContext.Categories.Update(category);
            return dbContext.SaveChanges();
        }
    }
}
