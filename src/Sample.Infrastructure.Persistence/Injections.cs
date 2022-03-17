using Microsoft.EntityFrameworkCore;
using Sample.Domain.Contacts;
using Sample.Infrastructure.Persistence.Contacts;
using Sample.Infrastructure.Persistence.Context;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Injections
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ISampleDbContext, SampleDbContext>(options => options.UseInMemoryDatabase("TestDb"));
            services.AddTransient<IContactRepository, ContactRepository>();
            return services;
        }
    }
}
