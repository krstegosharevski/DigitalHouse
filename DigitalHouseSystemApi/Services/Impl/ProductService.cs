using System.Xml.Linq;
using DigitalHouseSystemApi.Data;
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
        public readonly IColorService _colorService;
        public ProductService(
            IProductRepository productRepository, 
            ICategoryRepository categoryRepository, 
            IBrandRepository brandRepository,
            IPhotoService photoService,
            IColorService colorService) { 
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _photoService = photoService;
            _colorService = colorService;
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
            

            if (category == null)
            {
                throw new CategoryNotFoundException(productDto.CategoryId);
            }
            if (brand == null)
            {
                throw new BrandNotFoundException(productDto.BrandId);
            }

            int countProducts = 0;

            if (productDto.ColorIds != null)
            {
                countProducts = productDto.ColorIds.Count;
            }

            var product = new Product(productDto.Name, productDto.Price, productDto.Description, productDto.IsPresent,productDto.Quantity = countProducts, category, brand);

            _productRepository.Save(product);

            if (productDto.ColorIds != null && productDto.ColorIds.Any())
            {
                var colors = await _colorService.GetAllColorsByIdAsync(productDto.ColorIds);
                foreach (var color in colors)
                {
                    product.ProductColors.Add(new ProductColor
                    {
                        Product = product,
                        Color = color
                    });
                }
            }


            if (await _productRepository.SaveAllAsync())
            {
                if (file != null)
                {
                    var success = await AddPhotoAsync(file, product.Id); // product.Id бидејќи ID-то е креирано по Save
                    if (!success)
                    {
                        throw new Exception("Error adding photo to product");
                    }
                }
               
                return product.MappToDtoModel();
            }

            throw new Exception("Error saving product");
        }

        public async Task<ProductDto> EditProductAsync(int id, AddProductDto productDto, IFormFile file)
        {
            var prod = await _productRepository.FindByIdAsync(id);

            if (prod == null)
            {
                throw new ProductNotFoundException(id);
            }

            var category = await _categoryRepository.FindByIdAsync(productDto.CategoryId);
            var brand = await _brandRepository.FindByIdAsync(productDto.BrandId);


            if (category == null)
            {
                throw new CategoryNotFoundException(productDto.CategoryId);
            }
            if (brand == null)
            {
                throw new BrandNotFoundException(productDto.BrandId);
            }

            int countProducts = prod.Quantity;

            if (productDto.ColorIds != null)
            {
                countProducts = productDto.ColorIds.Count;
            }

            prod.Name = productDto.Name;
            prod.Description = productDto.Description;
            prod.Price = productDto.Price;
            prod.Quantity = countProducts;
            prod.IsPresent = productDto.IsPresent;
            prod.Category = category;
            prod.Brand = brand;

            if (productDto.ColorIds != null && productDto.ColorIds.Any())
            {
                prod.ProductColors.Clear();

                var colors = await _colorService.GetAllColorsByIdAsync(productDto.ColorIds);
                foreach (var color in colors)
                {
                    prod.ProductColors.Add(new ProductColor
                    {
                        Product = prod,
                        Color = color
                    });
                }
            }

            if (file != null) 
            {
                await DeletePhoto(id);

                var success = await AddPhotoAsync(file, id); 
                if (!success)
                {
                    throw new Exception("Error adding photo to product");
                }
            }

            if(await _productRepository.SaveAllAsync())
            {
                return prod.MappToDtoModel();
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
            ICollection<string> colors = prod.ProductColors.Select(color => color.Color).Select(color => color.HexCode).ToList();
            product = prod.MappToDtoModel();
            product.Colors = colors;    

            return product;
        }

        public async Task<GetProductEditDto> GetProductEditAsync(int id)
        {
            var prod = await _productRepository.FindByIdAsync(id);
            if (prod == null) {
                throw new ProductNotFoundException(id);
            }

            ICollection<int> colorIds = new List<int>();
            if (prod.ProductColors.Any())
            {
                foreach (var c in prod.ProductColors)
                {
                    colorIds.Add(c.ColorId);
                }
            }

            return new GetProductEditDto
            {
                Id = id,
                Name = prod.Name,
                Price = prod.Price,
                Description = prod.Description,
                IsPresent = prod.IsPresent,
                PhotoUrl = prod.Photo?.Url,
                Category = prod.CategoryId,
                Brand = prod.BrandId,
                Colors = colorIds
            };
           
        }
    }
}

