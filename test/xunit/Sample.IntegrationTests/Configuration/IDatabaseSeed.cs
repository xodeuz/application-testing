using System;

namespace Sample.XUnit.IntegrationTests.Configuration
{
    /// <summary>
    ///     Abstraction for custom database seeding
    /// </summary>
    public interface IDatabaseSeed
    {
        void Execute(IServiceProvider services);
    }
}
