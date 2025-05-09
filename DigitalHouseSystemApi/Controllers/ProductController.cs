﻿using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Extensions;
using DigitalHouseSystemApi.Helpers;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Models.Exceptions;
using DigitalHouseSystemApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DigitalHouseSystemApi.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IPhotoService _photoService;
        
        public ProductController(IProductRepository productRepository, IProductService productService , IPhotoService photoService)
        {
            _productRepository = productRepository;
            _productService = productService;
            _photoService = photoService;
        }

        [HttpPost("add-image")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file, int id)
        {
            var product = await _productService.FindByIdAsync(id);

            if (product == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            product.Photo = photo;

            if (await _productRepository.SaveAllAsync()) return Ok(new PhotoDto
            {
                Id = photo.Id,
                Url = photo.Url
            });

            return BadRequest("problem adding photo!");
            //try
            //{
            //    var success = await _productService.AddPhotoAsync(file, id);

            //    if (success)
            //    {
            //        return Ok();
            //    }

            //    return StatusCode(500, "Error: Could not save photo.");
            //}
            //catch (ArgumentException ex)
            //{
            //    return NotFound(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int productId)
        {
            if (productId == 0) return NotFound();

            try
            {
                await _photoService.DeletePhotoAsync(productId);

                await _productService.DeletePhoto(productId);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("add-product")]
        public async Task<ActionResult<ProductDto>> AddProduct([FromForm] IFormCollection form, [FromForm] IFormFile? file)
        {
            if (form == null)
            {
                return BadRequest("Invalid product data");
            }

            try
            {
                var productColorsJson = form["ProductColors"];
                var productColors = JsonConvert.DeserializeObject<List<ProductColorDto>>(productColorsJson!);

                var productDto = new AddProductDto
                {
                    Name = form["Name"],
                    Price = double.Parse(form["Price"]),
                    Description = form["Description"],
                    IsPresent = bool.Parse(form["IsPresent"]),
                    Quantity = int.TryParse(form["Quantity"], out int qty) ? qty : 0,
                    CategoryId = int.Parse(form["CategoryId"]),
                    BrandId = int.Parse(form["BrandId"]),
                    ProductColors = productColors!
                };
                // thorough the service; adding.
                var addedProduct = await _productService.AddProductAsync(productDto, file);
               
                return Ok(addedProduct);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BrandNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("edit-product")]
        public async Task<ActionResult<ProductDto>> EditProduct(
                                    [FromQuery] int productId,
                                    [FromForm] IFormCollection form, 
                                    [FromForm] IFormFile? file = null)
        {

            if (form == null)
            {
                return BadRequest("Invalid product data");
            }

            try
            {
                var productColorsJson = form["ProductColors"];
                var productColors = JsonConvert.DeserializeObject<List<ProductColorDto>>(productColorsJson!);

                var productDto = new AddProductDto
                {
                    Name = form["Name"],
                    Price = double.Parse(form["Price"]),
                    Description = form["Description"],
                    IsPresent = bool.Parse(form["IsPresent"]),
                    Quantity = int.TryParse(form["Quantity"], out int qty) ? qty : 0,
                    CategoryId = int.Parse(form["CategoryId"]),
                    BrandId = int.Parse(form["BrandId"]),
                    ProductColors = productColors!
                };

                var editedProduct = await _productService.EditProductAsync(productId, productDto, file);

                return Ok(editedProduct);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BrandNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            IEnumerable<Product> products = await _productService.GetAllProductsAsync();
            
            if(products == null) return NotFound();

            List<ProductDto> productsDto = new List<ProductDto>();

            foreach (var product in products)
            {
                productsDto.Add(product.MappToDtoModel());
            }

            return Ok(productsDto);
        }

        [HttpGet("category")]
        public async Task<ActionResult<PagedList<ProductDto>>> GetProductsByCategory([FromQuery] string category, [FromQuery] ProductParams productParams)
        {
            try
            {
                
                var products = await _productService.GetAllProductsByCategoryAsync(category, productParams);
                Response.AddPaginationHeader(new PaginationHeader(products.CurrentPage, products.PageSize, products.TotalCount, products.TotalPages));

                return Ok(products);
            }
            catch (CategoryNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<ICollection<SearchProductDto>>> GetSearchedProducts([FromQuery] string search)
        {
            try
            {
                var products = await _productService.GetAllSearchProductsAsync(search);
                return Ok(products);
            }
            catch (SearchNotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search-name")]
        public async Task<ActionResult<ProductDto>> GetSearchedProduct([FromQuery] string name)
        {
            try
            {
                var product = await _productService.GetSearchedProductAsync(name);
                return Ok(product);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-product-edit")]
        public async Task<ActionResult<GetProductEditDto>> GetProductForEdit([FromQuery] int id)
        {
            try
            {
                var product = await _productService.GetProductEditAsync(id);
                return Ok(product);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
