using Permission.Domain.Entities;
using Permission.Domain.Ports;

namespace Permission.Infrastructure.Adapters
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<PermissionEntity> PermissionsEntityRepository { get; }
        public IGenericRepository<PermissionTypeEntity> PermissionTypeEntityRepository { get; }
        public IPermissionElasticRepository PermissionElasticRepository { get; }

        public UnitOfWork(
            IGenericRepository<PermissionEntity> permissionsEntityRepository,
            IGenericRepository<PermissionTypeEntity> permissionTypeEntityRepository,
            IPermissionElasticRepository permissionElasticRepository)
        {
            PermissionsEntityRepository = permissionsEntityRepository;
            PermissionTypeEntityRepository = permissionTypeEntityRepository;
            PermissionElasticRepository = permissionElasticRepository;
        }

        public async Task RequestPermissionAsync(PermissionEntity permission)
        {
            await PermissionsEntityRepository.AddAsync(permission);
            // await PermissionElasticRepository.IndexPermissionAsync(permission);
        }

        public async Task UpdatePermissionAsync(PermissionEntity permission)
        {
            await PermissionsEntityRepository.UpdateAsync(permission);
            // await PermissionElasticRepository.IndexPermissionAsync(permission);
        }

        public async Task<PermissionEntity> GetPermissionAsync(int userId)
        {
            var permission = await PermissionsEntityRepository.GetByIdAsync(userId);
            // await PermissionElasticRepository.IndexPermissionAsync(permission);
            return permission;
        }

        public async Task<PermissionTypeEntity> GetPermissionTypeAsync(int permissionType)
        {
            var permission = await PermissionTypeEntityRepository.GetByIdAsync(permissionType);
            // await PermissionElasticRepository.IndexPermissionTypeAsync(permission);
            return permission;
        }

        public async Task<PermissionTypeEntity> AddPermissionTypeAsync(PermissionTypeEntity permissionTypeEntity)
        {
            // await PermissionElasticRepository.IndexPermissionTypeAsync(permissionTypeEntity);
            return await PermissionTypeEntityRepository.AddAsync(permissionTypeEntity);
        }
    }
}
