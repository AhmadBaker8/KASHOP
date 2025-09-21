using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface ICartService
    {
        bool AddToCart(CartRequest request, string userId);

        CartSummaryResponse CartSummaryResponse(string userId);

    }
}
