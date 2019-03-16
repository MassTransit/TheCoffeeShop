namespace TheCoffeeShop.Service
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using MassTransit;
    using MassTransit.ExtensionsLoggingIntegration;
    using MassTransit.Logging;
    using MassTransit.Logging.Tracing;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;


    public class MassTransitConsoleHostedService :
        IHostedService
    {
        readonly IBusControl _bus;

        public MassTransitConsoleHostedService(IBusControl bus, ILoggerFactory loggerFactory)
        {
            _bus = bus;

            if (loggerFactory != null && Logger.Current.GetType() == typeof(TraceLogger))
                ExtensionsLogger.Use(loggerFactory);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _bus.StartAsync(cancellationToken).ConfigureAwait(false);

            await _bus.Publish<SubmitOrder>(new
            {
                OrderId = NewId.NextGuid(),
                Timestamp = DateTime.UtcNow,
            }, cancellationToken).ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _bus.StopAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}