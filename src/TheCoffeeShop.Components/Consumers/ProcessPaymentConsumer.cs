namespace TheCoffeeShop.Components.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using MassTransit;
    using MassTransit.Courier;


    public class ProcessPaymentConsumer :
        IConsumer<ProcessOrderPayment>
    {
        public async Task Consume(ConsumeContext<ProcessOrderPayment> context)
        {
            RoutingSlipBuilder builder = new RoutingSlipBuilder(context.Message.CommandId);

            builder.AddActivity("LoyaltyPayment", new Uri("loopback://localhost/loyalty-payment-execute"), new { });

            builder.SetVariables(new {context.Message.Order});

            await context.Execute(builder.Build());
        }
    }
}