using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MultipleImplementation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            services.AddSingleton<ShoppingCartCache>();
            services.AddSingleton<ShoppingCartDB>();
            services.AddSingleton<ShoppingCartAPI>();

            services.AddTransient<Func<string, IShoppingCart>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "API":
                        return serviceProvider.GetService<ShoppingCartAPI>();
                    case "DB":
                        return serviceProvider.GetService<ShoppingCartDB>();
                    default:
                        return serviceProvider.GetService<ShoppingCartCache>();
                }
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
