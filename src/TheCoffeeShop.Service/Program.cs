namespace TheCoffeeShop.Service
{
    using System;
    using System.Threading.Tasks;
    using Components.Consumers;
    using Components.StateMachines;
    using MassTransit;
    using MassTransit.Definition;
    using MassTransit.Saga;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;


    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", true);
                    config.AddEnvironmentVariables();

                    if (args != null)
                        config.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<AppConfig>(hostContext.Configuration.GetSection("AppConfig"));

                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddConsumersFromNamespaceContaining<ConsumerAnchor>();
                        cfg.AddSagaStateMachinesFromNamespaceContaining<StateMachineAnchor>();
                        cfg.AddBus(ConfigureBus);
                    });

                    services.AddSingleton(typeof(ISagaRepository<>), typeof(InMemorySagaRepository<>));

                    services.AddSingleton<IHostedService, MassTransitConsoleHostedService>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            await builder.RunConsoleAsync().ConfigureAwait(false);
        }

        static IBusControl ConfigureBus(IServiceProvider provider)
        {
            var options = provider.GetRequiredService<IOptions<AppConfig>>().Value;

            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(options.Host, options.VirtualHost, h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });

                cfg.UseInMemoryScheduler();

                cfg.ConfigureEndpoints(provider, new KebabCaseEndpointNameFormatter());
            });
        }
    }
}