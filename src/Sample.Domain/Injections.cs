using Sample.Domain.Contacts;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Injections
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
            => services.AddTransient<IContactService, ContactService>();
    }
}
