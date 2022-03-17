using Microsoft.Extensions.DependencyInjection;
using Sample.Domain.Contacts;
using Sample.Infrastructure.Persistence.Context;
using Sample.IntegrationTests.Configuration;
using System;

namespace Sample.IntegrationTests.Contacts.Seeds
{
    internal class ContactDatabaseSeed : IDatabaseSeed
    {
        public void Execute(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var databaseContext = scope.ServiceProvider.GetRequiredService<ISampleDbContext>();

            databaseContext.Contacts.Add(Contact.New("Chris P. Bacon", "test@test.com", "0734100100"));
            databaseContext.SaveChangesAsync().Wait();
        }
    }
}
