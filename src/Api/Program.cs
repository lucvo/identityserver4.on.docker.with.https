// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "API";

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            // Calling UseKestrel to set https configuration
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel(options =>
            {
                options.Listen(IPAddress.Any, 443, listenOptions =>
                {
                    var configuration = (IConfiguration)options.ApplicationServices.GetService(typeof(IConfiguration));

                    listenOptions.UseHttps("cert.pfx", configuration["certPassword"]);
                });
            })
            .UseStartup<Startup>()
            .Build();
    }
}