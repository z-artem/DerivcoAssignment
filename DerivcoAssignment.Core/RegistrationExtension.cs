using DerivcoAssignment.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DerivcoAssignment.Core
{
    public static class RegistrationExtension
    {
        public static void RegisterDependencies(this IServiceCollection dependencies)
        {
            dependencies.AddTransient<IFibonacciGenerator, FibonacciGenerator>();

            dependencies.AddSingleton<INumbersCache, NumbersCache>();
        }
    }
}
