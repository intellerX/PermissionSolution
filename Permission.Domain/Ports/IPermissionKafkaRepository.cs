using Permission.Domain.Entities;

namespace Permission.Domain.Ports
{
    public interface IPermissionKafkaRepository
    {
        Task PublishOperationAsync(PermissionOperationMessageEntity message);
    }
}
