using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class ProductService : GenericService<ProductRequest,ProductResponse,Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productRepository, IFileService fileService) :base(productRepository)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }

        public async Task<int> CreateProduct(ProductRequest request)
        {
            var entity = request.Adapt<Product>();
            entity.CreatedAt = DateTime.UtcNow;
            if(request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                entity.MainImage = imagePath;
            }
            if(request.SubImages != null)
            {
                var subImagePaths = await _fileService.UploadManyAsync(request.SubImages);
                entity.SubImages = subImagePaths.Select(img => new ProductImage { ImageName = img }).ToList(); ;
            }
            return _productRepository.Add(entity);
        }

        public async Task<List<ProductResponse>> GetAllProducts(HttpRequest httpRequest,bool onlyActive = false)
        {
            var products = _productRepository.GetAllProductsWithImages();
            if (onlyActive)
            {
                products = products.Where(p => p.Status == Status.Active).ToList();
            }
            return products.Select( p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity,
                MainImageUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/images/{p.MainImage}",
                SubImagesUrls = p.SubImages?.Select(si => $"{httpRequest.Scheme}://{httpRequest.Host}/Images/{si.ImageName}").ToList(),
            }).ToList();

        }


    }
}
