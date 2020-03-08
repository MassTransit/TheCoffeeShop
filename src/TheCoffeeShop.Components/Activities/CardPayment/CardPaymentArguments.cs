namespace TheCoffeeShop.Components.Activities.CardPayment
{
    using Contracts.Models;


    public interface CardPaymentArguments
    {
        Order Order { get; }

        CardPaymentInfo PaymentInfo { get; }

        Adjustment[] Adjustments { get; }

        PaymentDue PaymentDue { get; }
    }
}