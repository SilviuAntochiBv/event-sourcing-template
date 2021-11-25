using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Consul;

using EventBasedServiceTemplate.Domain.Contracts.Persistence;

namespace EventBasedServiceTemplate.Persistence.Repositories
{
    public class RuntimeConfigurationStore : IRuntimeConfigurationStore
    {
        private readonly IConsulClient _consulClient;
        private readonly string _configurationAppPrefix;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RuntimeConfigurationStore(IConsulClient consulClient, string configurationAppPrefix, JsonSerializerOptions jsonSerializerOptions)
        {
            _consulClient = consulClient;
            _configurationAppPrefix = configurationAppPrefix;
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public async Task<T> GetConfiguration<T>(string key) where T : class, new()
        {
            var fullConfigKey = $"{_configurationAppPrefix}/{key}";

            var configQueryResult = await _consulClient.KV.Get(fullConfigKey);

            var configRawData = Encoding.UTF8.GetString(configQueryResult.Response.Value);

            return JsonSerializer.Deserialize<T>(configRawData, _jsonSerializerOptions);
        }
    }
}
