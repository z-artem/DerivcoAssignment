using DerivcoAssignment.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DerivcoAssignment.Core
{
    public delegate INumbersCache CacheResolver(bool useCache);

    public static class RegistrationExtension
    {
        public static void RegisterDependencies(this IServiceCollection dependencies)
        {
            dependencies.AddTransient<IFibonacciGenerator, FibonacciGenerator>();

            dependencies.AddSingleton<NumbersCache>();
            dependencies.AddSingleton<DummyCache>();

            dependencies.AddTransient<CacheResolver>(sp => useCache => useCache ? sp.GetService<NumbersCache>() : sp.GetService<DummyCache>());
        }
    }
}
