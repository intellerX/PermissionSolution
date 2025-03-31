using Permission.Domain.Entities;

namespace Permission.Domain.Ports
{
    public interface IUnitOfWork
    {
        Task<PermissionTypeEntity> AddPermissionTypeAsync(PermissionTypeEntity permissionTypeEntity);
        Task<PermissionEntity> GetPermissionAsync(int userId);
        Task<PermissionTypeEntity> GetPermissionTypeAsync(int permissionType);
        Task RequestPermissionAsync(PermissionEntity permission);
        Task UpdatePermissionAsync(PermissionEntity permission);
    }
}
