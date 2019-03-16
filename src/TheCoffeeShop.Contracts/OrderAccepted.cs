namespace TheCoffeeShop.Contracts
{
    using System;


    public interface OrderAccepted
    {
        Guid OrderId { get; }

        DateTime Timestamp { get; }
    }
}