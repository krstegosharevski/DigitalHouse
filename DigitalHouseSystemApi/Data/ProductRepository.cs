using DigitalHouseSystemApi.DTOs;
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

        public async Task<IEnumerable<Product>> GetAllProductsByCategoryIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Photo)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductColors)
                .ThenInclude(p => p.Color)
                .Where(p => p.CategoryId == id)
                .ToListAsync();
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
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
            return product;
        }

        public async Task<IEnumerable<Product>> SearchByNameProductsAsync(string search)
        {
            return await _context.Products.Include(p => p.Photo).Where(p => p.Name.Contains(search)).Take(5).ToListAsync();
        }
    }
}
