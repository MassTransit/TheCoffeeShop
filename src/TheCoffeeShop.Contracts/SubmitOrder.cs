namespace TheCoffeeShop.Contracts
{
    using System;


    public interface SubmitOrder
    {
        Guid OrderId { get; }

        DateTime Timestamp { get; }
    }
}