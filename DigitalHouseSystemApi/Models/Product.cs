namespace DigitalHouseSystemApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsPresent { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public Photo? Photo { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();


        public Product() 
        {
            CreatedAt = DateTime.UtcNow;
        }
        public Product(string name, double price, string description, bool ispresent, int quantity, Category category, Brand brand)
        {
            Name = name;
            Price = price;
            Description = description;
            IsPresent = ispresent;
            Quantity = quantity;
            CreatedAt = DateTime.UtcNow;
            Category = category;
            Brand = brand;
        }

      
    }
}
