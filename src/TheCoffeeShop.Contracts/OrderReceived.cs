namespace TheCoffeeShop.Contracts
{
    using System;
    using Models;


    public interface OrderReceived
    {
        Guid OrderId { get; }

        DateTime Timestamp { get; }

        Order Order { get; }
    }
}