namespace TheCoffeeShop.Contracts.Models
{
    using System;


    public interface Adjustment
    {
        Guid AdjustmentId { get; }

        AdjustmentLine[] AdjustmentLines { get; }
    }
}