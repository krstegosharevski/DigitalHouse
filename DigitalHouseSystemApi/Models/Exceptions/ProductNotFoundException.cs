﻿namespace DigitalHouseSystemApi.Models.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string name)
           : base($"Product with Name: {name} was not found.")
        {
        }

        public ProductNotFoundException(int id)
          : base($"Product with Id: {id} was not found.")
        {
        }
    }
}
