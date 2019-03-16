namespace TheCoffeeShop.Contracts
{
    using System;
    using Models;


    public interface ProcessOrderPayment
    {
        Guid CommandId { get; }

        Order Order { get; }
    }
}