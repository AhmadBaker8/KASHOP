using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Classes;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class BrandService : GenericService<BrandRequest, BrandResponse,Brand>,IBrandService
    {
        private readonly IFileService _fileService;
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository, IFileService fileService) : base(brandRepository)
        {
            _fileService = fileService;
            _brandRepository = brandRepository;
        }
        public async Task<int> CreateFile(BrandRequest request)
        {
            var entity = request.Adapt<Brand>();
            entity.CreatedAt = DateTime.UtcNow;
            if (request.Image != null)
            {
                var imagePath = await _fileService.UploadAsync(request.Image);
                entity.Image = imagePath;
            }
            return _brandRepository.Add(entity);
        }
    }
}
