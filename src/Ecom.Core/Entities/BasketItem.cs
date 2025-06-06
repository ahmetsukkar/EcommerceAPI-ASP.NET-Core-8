﻿namespace Ecom.Core.Entities
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductPicture { get; set; }  
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }
}