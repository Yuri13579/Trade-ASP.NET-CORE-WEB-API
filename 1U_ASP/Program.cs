using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace _1U_ASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((context, builder) =>
                {
                    // Данный код существует в методе CreateDefaultBuilder и добавлен для примера
                    // В секции Logging файла appsettigns.json определены настройки системы логирования
                    builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                    builder.AddFile(); // NuGet Package NetEscapades.Extensions.Logging.RollingFile
                })
                .UseStartup<Startup>();
    }
}
