using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Helpers;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHouseSystemApi.Data
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeletePhoto(Photo photo)
        {
            _context.Photos.Remove(photo);
        }

        public async Task<Product?> FindByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Photo)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductColors)
                .ThenInclude(p => p.Color)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Photo)
                .Include(p=>  p.Category)
                .Include(p => p.Brand)
                .ToListAsync();
        }

        public async Task<PagedList<Product>> GetAllProductsByCategoryIdAsync(int id, ProductParams productParams)
        {
            var query = _context.Products
                .Include(p => p.Photo)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductColors)
                .ThenInclude(p => p.Color)
                .Where(p => p.CategoryId == id)
                .AsQueryable();

            if (productParams.BrandIds != null && productParams.BrandIds.Any())
            {
                query = query.Where(p => productParams.BrandIds.Contains(p.BrandId));
            }
            if (productParams.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= productParams.MinPrice.Value);
            }
            if (productParams.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= productParams.MaxPrice.Value);
            }

            //var query = _context.Products
            //    .Include(p => p.Photo)
            //    .Include(p => p.Category)
            //    .Include(p => p.Brand)
            //    .Include(p => p.ProductColors)
            //    .ThenInclude(p => p.Color)
            //    .Where(p => p.CategoryId == id)
            //    .AsNoTracking();


            return await PagedList<Product>.CreateAsync(query,productParams.PageNumber, productParams.PageSize);         
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Save(Product product)
        {
            _context.Add(product);
        }

        public async Task<Product> FindProductByName(string name)
        {
            var product = await _context.Products
                .Include(p => p.Photo)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductColors)
                .ThenInclude(p => p.Color)
                .FirstOrDefaultAsync(p => p.Name == name);

            return product;
        }

        public async Task<IEnumerable<Product>> SearchByNameProductsAsync(string search)
        {
            //return await _context.Products.Include(p => p.Photo).Where(p => p.Name.ToLower().Contains(search.ToLower())).Take(7).ToListAsync();
            return await _context.Products
                .Include(p => p.Photo)
                .Include(p => p.Brand)
                .Where(p => EF.Functions.Like(p.Name, $"%{search}%")
                    || EF.Functions.Like(p.Brand.Name, $"%{search}%"))
                .Take(7)
                .ToListAsync();
        }
    }
}
