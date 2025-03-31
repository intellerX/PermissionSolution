using Permission.Domain.Entities;
using Permission.Domain.Ports;

namespace Permission.Infrastructure.Adapters
{
    public class PermissionElasticRepository : IPermissionElasticRepository
    {
        public Task IndexPermissionAsync(PermissionEntity permission)
        {
            throw new NotImplementedException();
        }

        public Task IndexPermissionTypeAsync(PermissionTypeEntity permission)
        {
            throw new NotImplementedException();
        }
    }
}
