namespace Permission.Domain.Entities
{
    public class PermissionOperationMessageEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Operation { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
