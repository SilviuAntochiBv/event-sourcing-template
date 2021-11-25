using EventBasedServiceTemplate.Domain;
using EventBasedServiceTemplate.Behavior;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DIRegistrationHelper
    {
        public static void RegisterServicesComponents(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IUniqueIdGenerator, UniqueIdGenerator>();
        }
    }
}
