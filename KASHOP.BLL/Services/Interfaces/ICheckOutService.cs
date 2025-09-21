using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Interfaces
{
    public interface ICheckOutService
    {
        Task<CheckOutResponse> ProccessPaymentAsync(CheckOutRequest request,string userId,HttpRequest request1);
    }
}
