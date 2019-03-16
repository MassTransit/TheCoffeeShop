namespace TheCoffeeShop.Components.Activities.CardPayment
{
    public interface CardPaymentLog
    {
        string TransactionId { get; }
        decimal Amount { get; }
    }
}