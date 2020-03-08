namespace TheCoffeeShop.Service
{
    using System.Threading;
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;


    public class MassTransitConsoleHostedService :
        IHostedService
    {
        readonly IBusControl _bus;

        public MassTransitConsoleHostedService(IBusControl bus, ILoggerFactory loggerFactory)
        {
            _bus = bus;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _bus.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _bus.StopAsync(cancellationToken);
        }
    }
}