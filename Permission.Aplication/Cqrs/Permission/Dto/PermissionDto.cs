namespace Permission.Aplication.Cqrs.Permission.Dto
{
    public class PermissionDto
    {
        public int? Id { get; set; }

        public required string EmployeeForename { get; set; }

        public required string EmployeeSurname { get; set; }

        public required int PermissionTypeId { get; set; }

        public DateTime StartDate { get; set; }
    }
}
