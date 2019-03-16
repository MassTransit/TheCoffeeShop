namespace TheCoffeeShop.Components.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using MassTransit;
    using MassTransit.Definition;
    using Microsoft.Extensions.Logging;


    public class SubmitOrderConsumer :
        IConsumer<SubmitOrder>
    {
        readonly ILogger<SubmitOrderConsumer> _log;

        public SubmitOrderConsumer(ILoggerFactory loggerFactory)
        {
            _log = loggerFactory.CreateLogger<SubmitOrderConsumer>();
        }

        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            using (_log.BeginScope("SubmitOrder {OrderId}", context.Message.OrderId))
            {
                await context.Publish<OrderReceived>(new
                {
                    context.Message.OrderId,
                    ReceiveTime = DateTime.UtcNow,
                });

                if (_log.IsEnabled(LogLevel.Debug))
                    _log.LogDebug("Validating order {OrderId}", context.Message.OrderId);

                // do some validation, is it a valid order, etc.

                await context.Publish<OrderAccepted>(new
                {
                    context.Message.OrderId,
                    ReceiveTime = DateTime.UtcNow,
                });

                await context.RespondAsync<OrderSubmitted>(new
                {
                    context.Message.OrderId,
                    ReceiveTime = DateTime.UtcNow,
                });

                if (_log.IsEnabled(LogLevel.Debug))
                    _log.LogDebug("Accepted order {OrderId}", context.Message.OrderId);
            }
        }
    }


    public class SubmitOrderConsumerDefinition :
        ConsumerDefinition<SubmitOrderConsumer>
    {
        public SubmitOrderConsumerDefinition()
        {
            ConcurrentMessageLimit = 10;
        }
    }
}