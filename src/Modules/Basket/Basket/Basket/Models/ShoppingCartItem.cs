﻿namespace Basket.Basket.Models
{
    public class ShoppingCartItem : Entity<Guid>
    {
        internal ShoppingCartItem(Guid shoppingCartId, Guid productId, 
            int quantity, string color, decimal price, string productName)
        {
            ShoppingCartId = shoppingCartId;
            ProductId = productId;
            Quantity = quantity;
            Color = color;
            Price = price;
            ProductName = productName;
        }

        public Guid ShoppingCartId { get; private set; } = default!;
        public Guid ProductId { get; private set; } = default!;
        public string Color { get; private set; } = default!;
        public int Quantity { get; internal set; } = default!; //apenas será modificado no assembly basket

        //virá do Catalog Module
        public string ProductName { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
        
    }
}