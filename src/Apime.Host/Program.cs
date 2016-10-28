using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Apime.Host
{
    public class Program
    {
        public static string ApiPluginPath;

        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("please provide port and path to the plugin folder");
                Console.WriteLine(" > dotnet run [port] [plugin-path]");
                return;
            }

            var port = args[0];
            ApiPluginPath = args[1];

            var hostUrls = new []
            {
                string.Format("http://localhost:{0}", port),
                // string.Format("http://0.0.0.0:{0}", port),
                // string.Format("http://127.0.0.1:{0}", port)
            };

            Console.WriteLine(" > api endpoint: {0}", string.Join(",", hostUrls));
            Console.WriteLine(" > plugin path : {0}", ApiPluginPath);

            var config = new ConfigurationBuilder()
                //.AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseUrls(hostUrls)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
