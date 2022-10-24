
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace BookStoreApi
{
    /// <summary>
    /// </summary>
    public class Program
    {
        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .ConfigureLogging(
                            l =>
                            {
                                l.AddApplicationInsights();
                                l.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Information);
                            });
                });
    }
}
