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
        public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
        {
           
        }
        
    }
}
