namespace Permission.Domain.Entities
{
    public class PermissionEntity : BaseEntity
    {
        public required string EmployeeForename { get; set; }

        public required string EmployeeSurname { get; set; }

        public required int PermissionTypeId { get; set; }

        public required PermissionTypeEntity PermissionType { get; set; }

        public DateTime StartDate { get; set; }
    }
}
