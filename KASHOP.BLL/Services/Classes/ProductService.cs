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

        public async Task<List<ProductResponse>> GetAllProducts(HttpRequest httpRequest, int pageNumber = 1, int pageSize = 1, bool onlyActive = false)
        {
            var products = await _productRepository.GetAllProductsAsync();
            if (onlyActive)
            {
                products = products.Where(p => p.Status == Status.Active).ToList();
            }

            var pagedProducts = products.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

            return MapProductsToResponse(httpRequest, pagedProducts);
        }

        public async Task<ProductResponse?> GetById(HttpRequest httpRequest, int id)
        {
            var products = await _productRepository.GetAllProductsAsync();
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null) return null;

            return MapProductsToResponse(httpRequest, new List<Product> { product }).FirstOrDefault();
        }
        public async Task<List<ProductResponse>> GetByCategory(HttpRequest httpRequest, int categoryId, int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            

            return MapProductsToResponse(httpRequest, products);
        }
        public async Task<List<ReviewResponse>> GetReviews(int productId)
        {
            var products = await _productRepository.GetAllProductsAsync();
            var productsReview = products.FirstOrDefault(p => p.Id == productId);

            if (productsReview == null || productsReview.Reviews == null)
                return new List<ReviewResponse>();

            return productsReview.Reviews.Select(r => new ReviewResponse
            {
                Id = r.Id,
                FullName = r.User.FullName,
                Comment = r.Comment,
                Rate = r.Rate
            }).ToList();
        }
        




        private List<ProductResponse> MapProductsToResponse(HttpRequest httpRequest, List<Product> products)
        {
            return products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity,
                MainImageUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/images/{p.MainImage}",
                SubImagesUrls = p.SubImages?.Select(si => $"{httpRequest.Scheme}://{httpRequest.Host}/images/{si.ImageName}").ToList(),
                Description = p.Description,
                Price = p.Price,
                Discount = p.Discount,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name,
                CreatedAt = p.CreatedAt,
                Reviews = p.Reviews?.Select(r => new ReviewResponse
                {
                    Id = r.Id,
                    FullName = r.User.FullName,
                    Comment = r.Comment,
                    Rate = r.Rate
                }).ToList()
            }).ToList();
        }


    }
}
