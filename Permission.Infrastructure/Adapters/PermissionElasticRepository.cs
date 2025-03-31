using Nest;
using Permission.Domain.Entities;
using Permission.Domain.Ports;

namespace Permission.Infrastructure.Adapters
{
    public class PermissionElasticRepository : IPermissionElasticRepository
    {
        private readonly IElasticClient _elasticClient;

        public PermissionElasticRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        }

        public async Task IndexPermissionAsync(PermissionEntity permission)
        {
            var indexExists = await _elasticClient.Indices.ExistsAsync("permissions");
            if (!indexExists.Exists)
            {
                await _elasticClient.Indices.CreateAsync("permissions", c => c.Map<PermissionEntity>(m => m.AutoMap()));
            }

            var response = await _elasticClient.IndexDocumentAsync(permission);
            if (!response.IsValid)
            {
                throw new Exception($"Failed to index permission: {response.DebugInformation}");
            }
        }

        public async Task IndexPermissionTypeAsync(PermissionTypeEntity permissionType)
        {
            var indexExists = await _elasticClient.Indices.ExistsAsync("permissions");
            if (!indexExists.Exists)
            {
                await _elasticClient.Indices.CreateAsync("permissions", c => c.Map<PermissionEntity>(m => m.AutoMap()));
            }

            var response = await _elasticClient.IndexDocumentAsync(permissionType);
            if (!response.IsValid)
            {
                throw new Exception($"Failed to index permission: {response.DebugInformation}");
            }
        }
    }
}
