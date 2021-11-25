using System.Threading.Tasks;

namespace EventBasedServiceTemplate.Domain.Contracts.Persistence
{
    public interface IRuntimeConfigurationStore
    {
        Task<T> GetConfiguration<T>(string key) where T : class, new();
    }
}
