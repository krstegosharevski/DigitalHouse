using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Models.Exceptions;

namespace DigitalHouseSystemApi.Services.Impl
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _productRepository;
        public readonly ICategoryRepository _categoryRepository;
        public readonly IBrandRepository _brandRepository;
        public readonly IPhotoService _photoService;
        public ProductService(
            IProductRepository productRepository, 
            ICategoryRepository categoryRepository, 
            IBrandRepository brandRepository,
            IPhotoService photoService) { 
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _photoService = photoService;
        }


        public async Task<Product> FindByIdAsync(int id)
        {
            return await _productRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsByCategoryAsync(string category)
        {
            Category c = await _categoryRepository.FindByNameAsync(category);
            if (c == null) throw new ArgumentException("Category not found");

            
            IEnumerable<Product> p = await _productRepository.GetAllProductsByCategoryIdAsync(c.Id);

            List<ProductDto> productsDto = new List<ProductDto>();

            foreach (var product in p) 
            {
                ICollection<string> colors = product.ProductColors.Select(color => color.Color).Select(color => color.HexCode).ToList();
                
                var productDto = product.MappToDtoModel();
                productDto.Colors = colors;
                productsDto.Add(productDto);
            }

            return productsDto;

        }

        public async Task<ProductDto> AddProductAsync(AddProductDto productDto, IFormFile file)
        {
            var category = await _categoryRepository.FindByIdAsync(productDto.CategoryId);
            var brand = await _brandRepository.FindByIdAsync(productDto.BrandId);

            if (category == null || brand == null)
            {
                throw new ArgumentException("Invalid category or brand");
            }

            var product = new Product(productDto.Name, productDto.Price, productDto.Description, productDto.IsPresent,productDto.Quantity, category, brand);

            _productRepository.Save(product);




            if (await _productRepository.SaveAllAsync())
            {
                var success = await AddPhotoAsync(file, product.Id); // product.Id бидејќи ID-то е креирано по Save
                if (!success)
                {
                    throw new Exception("Error adding photo to product");
                }
                return product.MappToDtoModel();
            }

            throw new Exception("Error saving product");
        }

        public async Task<bool> AddPhotoAsync(IFormFile file, int productId)
        {
            var product = await _productRepository.FindByIdAsync(productId);

            if (product == null) throw new ArgumentException("Error: Product not found");

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) throw new ArgumentException("Error: Can not add photo!");

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            product.Photo = photo;

            if (await _productRepository.SaveAllAsync()) return true;
            return false;
        }


        public async Task DeletePhoto(int id)
        {
            var product = await _productRepository.FindByIdAsync(id);

            if (product == null) throw new ArgumentException("Invalid category or brand");

            var photo = product.Photo;

            if (photo == null) throw new ArgumentException("Invalid category or brand");

            product.Photo = null;

            _productRepository.DeletePhoto(photo);

            await _productRepository.SaveAllAsync();
        }

        public async Task<ICollection<SearchProductDto>> GetAllSearchProductsAsync(string search)
        {
            var products = await _productRepository.SearchByNameProductsAsync(search);
            ICollection<SearchProductDto> searchedProducts = new List<SearchProductDto>();

            if (products == null || !products.Any()) { throw new SearchNotFoundException(search); }

            foreach (var product in products)
            {
                searchedProducts.Add(new SearchProductDto(product.Name, product.Photo.Url));
            }

            return searchedProducts;
        }

        public async Task<ProductDto> GetSearchedProductAsync(string name)
        {
            var prod = await _productRepository.FindProductByName(name);
            
            if(prod == null) { throw new ProductNotFoundException(name); }

            ProductDto product = new ProductDto();
            product = prod.MappToDtoModel();

            return product;
        }
    }
}
