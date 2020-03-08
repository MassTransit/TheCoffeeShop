namespace TheCoffeeShop.Components.Activities.CardPayment
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Models;
    using MassTransit.Courier;
    using MassTransit.Initializers;


    public class CardPaymentActivity :
        IActivity<CardPaymentArguments, CardPaymentLog>
    {
        public async Task<ExecutionResult> Execute(ExecuteContext<CardPaymentArguments> context)
        {
            var paymentDue = context.Arguments.PaymentDue;
            if (paymentDue == null)
                throw new ArgumentNullException(nameof(PaymentDue));

            string transactionId = "123456";

            var receipt = await MessageInitializerCache<PaymentReceipt>.InitializeMessage(new
            {
                Source = "Credit Card",
                paymentDue.Amount,
            });

            return context.CompletedWithVariables<CardPaymentLog>(new
            {
                TransactionId = transactionId,
                paymentDue.Amount
            }, new
            {
                PaymentReceipt = receipt
            });
        }

        public async Task<CompensationResult> Compensate(CompensateContext<CardPaymentLog> context)
        {
            // cancel authorization

            return context.Compensated();
        }
    }
}