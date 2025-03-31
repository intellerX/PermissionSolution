using Permission.Domain.Entities;

namespace Permission.Domain.Ports
{
    public interface IPermissionElasticRepository
    {
        Task IndexPermissionAsync(PermissionEntity permission);
        Task IndexPermissionTypeAsync(PermissionTypeEntity permission);
    }
}
