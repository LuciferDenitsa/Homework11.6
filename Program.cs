using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Homework11._6.Controllers;
using Homework11._6.Services;

namespace Homework11._6
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .UseConsoleLifetime()
                .Build();

            Console.WriteLine("Сервис запущен");
            await host.RunAsync();
            Console.WriteLine("Сервис закончен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStorage, MemoryStorage>();


            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyController>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("7581505209:AAGNsKbXu2n5tCz7UiZFrP-vhgc67m_sqIU"));
            services.AddHostedService<Bot>();
        }
    }
}
