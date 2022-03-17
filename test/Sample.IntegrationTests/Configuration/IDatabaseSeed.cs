using System;

namespace Sample.IntegrationTests.Configuration
{
    /// <summary>
    ///     Abstraction for custom database seeding
    /// </summary>
    public interface IDatabaseSeed
    {
        void Execute(IServiceProvider services);
    }
}
