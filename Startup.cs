using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace polly_getting_started
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHttpClient("GitHub", client =>
                {
                    client.BaseAddress = new Uri("https://api.github.com/");
                    client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                })
                .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.Retry(3));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) => { await context.Response.WriteAsync("Hello World!"); });
        }
    }


    public class SimpleCastClient
    {
        private HttpClient _client;

        public SimpleCastClient(HttpClient client, ILogger<SimpleCastClient> logger, IConfiguration config)
        {
            _client = client;
        }
    }
}