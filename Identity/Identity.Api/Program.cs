namespace Identity.Api
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
             WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, builder) =>
             {
                 IHostingEnvironment env = context.HostingEnvironment;
                 builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).
                    AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true,
                                                                      reloadOnChange: true)
                    .AddEnvironmentVariables();
             })
                 .UseStartup<Startup>()
                 .Build();
    }
}
