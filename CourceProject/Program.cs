using Laba_4;
using Microsoft.AspNetCore.Hosting;

namespace MyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBilder(string[] args) =>
        Host.CreateDefaultBuilder(args).
            ConfigureWebHostDefaults(webBilder => { webBilder.UseStartup<Startup>(); });
    }
}
