namespace TheCoffeeShop.Contracts
{
    using System;


    public interface OrderSubmitted
    {
        Guid OrderId { get; }
        DateTime Timestamp { get; }
    }
}