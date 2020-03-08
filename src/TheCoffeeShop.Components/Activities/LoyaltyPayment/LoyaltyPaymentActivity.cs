namespace TheCoffeeShop.Components.Activities.LoyaltyPayment
{
    using System.Threading.Tasks;
    using MassTransit;
    using MassTransit.Courier;


    public class LoyaltyPaymentActivity :
        IActivity<LoyaltyPaymentArguments, LoyaltyPaymentLog>
    {
        public async Task<ExecutionResult> Execute(ExecuteContext<LoyaltyPaymentArguments> context)
        {
            return context.Completed<LoyaltyPaymentLog>(new {PaymentId = NewId.NextGuid()});
        }

        public async Task<CompensationResult> Compensate(CompensateContext<LoyaltyPaymentLog> context)
        {
            return context.Compensated();
        }
    }
}