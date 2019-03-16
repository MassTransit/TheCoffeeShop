namespace TheCoffeeShop.Api
{
    using System.Threading;
    using System.Threading.Tasks;
    using MassTransit;
    using MassTransit.ExtensionsLoggingIntegration;
    using MassTransit.Logging;
    using MassTransit.Logging.Tracing;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;


    public class MassTransitApiHostedService :
        IHostedService
    {
        readonly IBusControl _bus;

        public MassTransitApiHostedService(IBusControl bus, ILoggerFactory loggerFactory)
        {
            _bus = bus;

            if (loggerFactory != null && Logger.Current.GetType() == typeof(TraceLogger))
                ExtensionsLogger.Use(loggerFactory);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _bus.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _bus.StopAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}