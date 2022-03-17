using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.Infrastructure.Persistence.Context;
using System;
using System.Linq;

namespace Sample.IntegrationTests.Configuration
{
    internal class WebApiApplicationFactory<TSeed> : WebApplicationFactory<Program> where TSeed : class, IDatabaseSeed
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                ReplaceDatabase(services);
                SeedDatabase(services);
            });
        }

        private static void SeedDatabase(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var seed = Activator.CreateInstance(typeof(TSeed)) as IDatabaseSeed;
            if (seed != null)
            {
                seed.Execute(sp);
            }
        }

        private static void ReplaceDatabase(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(
                                d => d.ServiceType ==
                                    typeof(DbContextOptions<SampleDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ISampleDbContext, SampleDbContext>(options =>
            {
                //
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });
        }
    }
}
