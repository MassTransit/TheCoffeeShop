namespace TheCoffeeShop.Contracts.Models
{
    using System;


    public interface Order
    {
        Guid OrderId { get; }

        OrderLine[] Lines { get; }
    }
}