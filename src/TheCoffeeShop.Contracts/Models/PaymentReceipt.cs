namespace TheCoffeeShop.Contracts.Models
{
    public interface PaymentReceipt
    {
        string Source { get; }
        decimal Amount { get; }
    }
}