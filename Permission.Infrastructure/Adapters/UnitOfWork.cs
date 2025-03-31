using Permission.Domain.Entities;
using Permission.Domain.Ports;

namespace Permission.Infrastructure.Adapters
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<PermissionEntity> PermissionsEntityRepository { get; }
        public IGenericRepository<PermissionTypeEntity> PermissionTypeEntityRepository { get; }
        public IPermissionElasticRepository PermissionElasticRepository { get; }
        public IPermissionKafkaRepository PermissionKafkaRepository { get; }


        public UnitOfWork(
            IGenericRepository<PermissionEntity> permissionsEntityRepository,
            IGenericRepository<PermissionTypeEntity> permissionTypeEntityRepository,
            IPermissionElasticRepository permissionElasticRepository,
            IPermissionKafkaRepository permissionKafkaRepository)
        {
            PermissionsEntityRepository = permissionsEntityRepository;
            PermissionTypeEntityRepository = permissionTypeEntityRepository;
            PermissionElasticRepository = permissionElasticRepository;
            PermissionKafkaRepository = permissionKafkaRepository;
        }

        public async Task RequestPermissionAsync(PermissionEntity permission)
        {
            await PermissionsEntityRepository.AddAsync(permission);
            var operation = new PermissionOperationMessageEntity()
            {
                Id = Guid.NewGuid(),
                Operation = "RequestPermissionAsync.Create",
                Payload = "permission",
                Timestamp = DateTime.UtcNow
            };
            await PermissionKafkaRepository.PublishOperationAsync(operation);
            await PermissionElasticRepository.IndexPermissionAsync(permission);
        }

        public async Task UpdatePermissionAsync(PermissionEntity permission)
        {
            await PermissionsEntityRepository.UpdateAsync(permission);
            var operation = new PermissionOperationMessageEntity()
            {
                Id = Guid.NewGuid(),
                Operation = "UpdateAsync.Update",
                Payload = "permission",
                Timestamp = DateTime.UtcNow
            };
            await PermissionKafkaRepository.PublishOperationAsync(operation);

            await PermissionElasticRepository.IndexPermissionAsync(permission);
        }

        public async Task<PermissionEntity> GetPermissionAsync(int userId)
        {
            var permission = await PermissionsEntityRepository.GetByIdAsync(userId);
            await PermissionElasticRepository.IndexPermissionAsync(permission);

            var operation = new PermissionOperationMessageEntity()
            {
                Id = Guid.NewGuid(),
                Operation = "Query",
                Payload = "GetPermissionAsync.Query",
                Timestamp = DateTime.UtcNow
            };
            await PermissionKafkaRepository.PublishOperationAsync(operation);
            return permission;
        }

        public async Task<PermissionTypeEntity> GetPermissionTypeAsync(int permissionType)
        {
            var permission = await PermissionTypeEntityRepository.GetByIdAsync(permissionType);
            await PermissionElasticRepository.IndexPermissionTypeAsync(permission);

            var operation = new PermissionOperationMessageEntity()
            {
                Id = Guid.NewGuid(),
                Operation = "Query",
                Payload = "GetPermissionTypeAsync.Query",
                Timestamp = DateTime.UtcNow
            };
            await PermissionKafkaRepository.PublishOperationAsync(operation);
            return permission;
        }

        public async Task<PermissionTypeEntity> AddPermissionTypeAsync(PermissionTypeEntity permissionTypeEntity)
        {
            await PermissionElasticRepository.IndexPermissionTypeAsync(permissionTypeEntity);

            var operation = new PermissionOperationMessageEntity()
            {
                Id = Guid.NewGuid(),
                Operation = "Add",
                Payload = "AddPermissionTypeAsync.Add",
                Timestamp = DateTime.UtcNow
            };
            await PermissionKafkaRepository.PublishOperationAsync(operation);

            return await PermissionTypeEntityRepository.AddAsync(permissionTypeEntity);
        }
    }
}
