namespace TheCoffeeShop.Components.Activities.LoyaltyPayment
{
    using Contracts;
    using Contracts.Models;


    public interface LoyaltyPaymentArguments
    {
        Order Order { get; }

        Adjustment[] Adjustments { get; }

        PaymentDue PaymentDue { get; }
    }
}