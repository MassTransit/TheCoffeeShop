namespace TheCoffeeShop.Contracts.Models
{
    public interface CardPaymentInfo
    {
        string VaultTokenId { get; }
        string PostalCode { get; }
        string SecurityCode { get; }
    }
}