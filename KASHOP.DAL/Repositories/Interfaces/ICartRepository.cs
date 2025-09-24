using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<int> AddAsync(Cart cart);
        Task<List<Cart>> GetUserCartAsync(string userId);

        Task<bool> ClearCartAsync(string userId);
    }
}
