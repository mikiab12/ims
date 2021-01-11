using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ims.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("hosting.json", optional: true, reloadOnChange: true)
                          .Build();

            //            var config = new ConfigurationBuilder()
            //                           .AddJsonFile("hosting.json", true)
            //                         .Build();

            var ret = WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<Startup>()
              ;

            return ret;
        }
    }
}
