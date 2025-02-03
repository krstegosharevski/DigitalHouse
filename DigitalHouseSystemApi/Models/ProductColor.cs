﻿namespace DigitalHouseSystemApi.Models
{
    public class ProductColor
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int ColorId { get; set; }
        public Color Color { get; set; } = null!;

        public ProductColor() { }

        public ProductColor(Product product, Color color)
        {
            Product = product;
            Color = color;
        }
    }
}
