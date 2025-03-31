using Permission.Domain.Entities;
using Permission.Domain.Ports;
using Serilog;

namespace Permission.Domain.Services
{
    [DomainService]
    public class PermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = Log.ForContext<PermissionService>();
        }

        public async Task<PermissionEntity> RequestPermissionAsync(PermissionEntity permission)
        {
            if (permission == null)
            {
                _logger.Error("RequestPermissionAsync: Received null permission entity");
                throw new ArgumentNullException(nameof(permission));
            }

            _logger.Information("Requesting permission for user {Id}", permission.Id);
            await _unitOfWork.RequestPermissionAsync(permission);
            _logger.Information("Permission request for user {Id} processed successfully", permission.Id);
            return permission;
        }

        public async Task<PermissionEntity> UpdatePermissionAsync(PermissionEntity permission)
        {
            if (permission == null)
            {
                _logger.Error("UpdatePermissionAsync: Received null permission entity");
                throw new ArgumentNullException(nameof(permission));
            }

            _logger.Information("Updating permission for user {Id}", permission.Id);
            await _unitOfWork.UpdatePermissionAsync(permission);
            _logger.Information("Permission update for user {Id} processed successfully", permission.Id);
            return permission;
        }

        public async Task<PermissionTypeEntity> GetPermissionTypeAsync(int permissionType)
        {
            if (permissionType == 0)
            {
                _logger.Error("GetPermissionTypeAsync: Invalid permissionType value 0");
                throw new ArgumentException("permissionType cannot be 0", nameof(permissionType));
            }

            _logger.Information("Fetching permission type for ID {PermissionType}", permissionType);
            var result = await _unitOfWork.GetPermissionTypeAsync(permissionType);
            _logger.Information("Fetched permission type: {Description}", result?.Description);
            return result;
        }

        public async Task<PermissionTypeEntity> AddPermissionTypeAsync(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                _logger.Error("AddPermissionTypeAsync: Received null or empty description");
                throw new ArgumentException("Description can't be null or empty", nameof(description));
            }

            _logger.Information("Adding new permission type with description: {Description}", description);
            var permissionTypeEntity = new PermissionTypeEntity { Description = description };
            var result = await _unitOfWork.AddPermissionTypeAsync(permissionTypeEntity);
            _logger.Information("Added permission type successfully with description: {Description}", result.Description);
            return result;
        }
    }
}
