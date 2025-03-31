using Permission.Domain.Entities;
using Permission.Domain.Ports;

namespace Permission.Domain.Services
{
    [DomainService]
    public class PermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<PermissionEntity> RequestPermissionAsync(PermissionEntity permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            await _unitOfWork.RequestPermissionAsync(permission);
            return permission;
        }

        public async Task<PermissionEntity> UpdatePermissionAsync(PermissionEntity permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            await _unitOfWork.UpdatePermissionAsync(permission);
            return permission;
        }

        public async Task<PermissionTypeEntity> GetPermissionTypeAsync(int permissionType)
        {
            if (permissionType == 0)
            {
                throw new ArgumentException("permissionType cannot be 0", nameof(permissionType));
            }

            return await _unitOfWork.GetPermissionTypeAsync(permissionType);
        }

        public async Task<PermissionTypeEntity> AddPermissionTypeAsync(string description)
        {
            if (description == null)
            {
                throw new ArgumentException("description cant be null", nameof(description));
            }

            var permissionTypeEntity = new PermissionTypeEntity() { Description = description };

            return await _unitOfWork.AddPermissionTypeAsync(permissionTypeEntity);
        }
    }
}
