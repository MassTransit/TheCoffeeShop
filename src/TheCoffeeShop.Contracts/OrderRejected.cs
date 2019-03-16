namespace TheCoffeeShop.Contracts
{
    using System;


    public interface OrderRejected
    {
        Guid OrderId { get; }
        DateTime Timestamp { get; }

        string Reason { get; }
    }
}