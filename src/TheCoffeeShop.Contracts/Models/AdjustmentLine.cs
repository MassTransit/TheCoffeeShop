namespace TheCoffeeShop.Contracts.Models
{
    using System;


    public interface AdjustmentLine
    {
        Guid AdjustmentLineId { get; }

        Guid OrderLineId { get; }

        string Description { get; }

        decimal Amount { get; }
    }
}