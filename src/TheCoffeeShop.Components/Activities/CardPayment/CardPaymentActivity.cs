namespace TheCoffeeShop.Components.Activities.CardPayment
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Models;
    using MassTransit.Courier;
    using MassTransit.Util;


    public class CardPaymentActivity :
        Activity<CardPaymentArguments, CardPaymentLog>
    {
        public async Task<ExecutionResult> Execute(ExecuteContext<CardPaymentArguments> context)
        {
            var paymentDue = context.Arguments.PaymentDue;
            if (paymentDue == null)
                throw new ArgumentNullException(nameof(PaymentDue));

            string transactionId = "123456";

            return context.CompletedWithVariables<CardPaymentLog>(new
            {
                TransactionId = transactionId,
                paymentDue.Amount
            }, new
            {
                PaymentReceipt = TypeMetadataCache<PaymentReceipt>.InitializeFromObject(new
                {
                    Source = "Credit Card",
                    paymentDue.Amount,
                })
            });
        }

        public async Task<CompensationResult> Compensate(CompensateContext<CardPaymentLog> context)
        {
            // cancel authorization

            return context.Compensated();
        }
    }
}