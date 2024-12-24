using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DigitalHouseSystemApi.Helpers;
using DigitalHouseSystemApi.Interfaces;
using Microsoft.Extensions.Options;

namespace DigitalHouseSystemApi.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IProductRepository _productRepository;

        public PhotoService(IOptions<CloudinarySettings> config, IProductRepository productRepository)
        {
            var acc = new Account
                (
                    config.Value.CloudName,
                    config.Value.ApiKey,
                    config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(acc);
            _productRepository = productRepository;
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if(file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder = "da-net7"
                };
                 uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(int productId)
        {
            var product = await _productRepository.FindByIdAsync(productId);

            if(product == null) throw new ArgumentException("Invalid product");

            var publicId = product.Photo?.PublicId ?? throw new Exception("Photo does not exist.");

            var deleteParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
