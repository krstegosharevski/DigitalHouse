using System.Xml.Linq;
using DigitalHouseSystemApi.Data;
using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Helpers;
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

        public async Task<PagedList<ProductDto>> GetAllProductsByCategoryAsync(string category, ProductParams productParams)
        {
            Category c = await _categoryRepository.FindByNameAsync(category);
            if (c == null) throw new CategoryNotFoundException(category);

            PagedList<Product> pagedProducts = await _productRepository.GetAllProductsByCategoryIdAsync(c.Id, productParams);


            List<ProductDto> productsDto = pagedProducts.Select(product =>
            {
                var productDto = product.MappToDtoModel();
                productDto.Colors = product.ProductColors.Select(pc => pc.Color.HexCode).ToList();
                return productDto;
            }).ToList();

            return new PagedList<ProductDto>(productsDto, pagedProducts.TotalCount, productParams.PageNumber, productParams.PageSize);

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

            var product = new Product(productDto.Name, productDto.Price, productDto.Description, productDto.IsPresent,productDto.Quantity = 0, category, brand);

            _productRepository.Save(product);

            if (productDto.ProductColors != null && productDto.ProductColors.Any())
            {
                var colorIds = productDto.ProductColors.Select(pc => pc.ColorId).ToList();
                var colors = await _colorService.GetAllColorsByIdAsync(colorIds);

                foreach (var pc in productDto.ProductColors)
                {
                    var color = colors.FirstOrDefault(c => c.Id == pc.ColorId);
                    if (color != null)
                    {
                        product.ProductColors.Add(new ProductColor
                        {
                            Product = product,
                            Color = color,
                            Quantity = pc.Quantity
                        });
                        countProducts += pc.Quantity;
                    }
                }
            }
            product.Quantity = countProducts;

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
                throw new CategoryNotFoundException(productDto.CategoryId);
            if (brand == null)
                throw new BrandNotFoundException(productDto.BrandId);

            prod.Name = productDto.Name;
            prod.Description = productDto.Description;
            prod.Price = productDto.Price;
           // prod.Quantity = productDto.Quantity;
            prod.IsPresent = productDto.IsPresent;
            prod.Category = category;
            prod.Brand = brand;
            int countProducts = 0;
            if (productDto.ProductColors != null && productDto.ProductColors.Any())
            {
                prod.ProductColors.Clear();

                foreach (var pc in productDto.ProductColors)
                {
                    prod.ProductColors.Add(new ProductColor
                    {
                        ProductId = id,
                        ColorId = pc.ColorId,
                        Quantity = pc.Quantity
                    });
                    countProducts += pc.Quantity;
                }
            }
            prod.Quantity = countProducts;

            if (file != null)
            {
                await DeletePhoto(id);
                var success = await AddPhotoAsync(file, id);
                if (!success)
                {
                    throw new Exception("Error adding photo to product");
                }
            }

            if (await _productRepository.SaveAllAsync())
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

            //ICollection<int> colorIds = new List<int>();
            List<ProductColorDto> colors = new List<ProductColorDto>();
            if (prod.ProductColors.Any())
            {
                foreach (var c in prod.ProductColors)
                {
                    //colorIds.Add(c.ColorId);
                    colors.Add(new ProductColorDto(c.ColorId, c.Quantity));
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
                ProductColors = colors
            };
           
        }
    }
}

