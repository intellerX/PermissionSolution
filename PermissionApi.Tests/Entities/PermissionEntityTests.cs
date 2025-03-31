using Permission.Domain.Entities;
using Xunit;

namespace PermissionApi.Tests.Entities
{
    public class PermissionEntityTests
    {
        [Fact]
        public void PermissionEntity_ShouldInitializeCorrectly()
        {
            var permissionType = new PermissionTypeEntity { Description = "Test Type" };
            var permission = new PermissionEntity
            {
                EmployeeForename = "John",
                EmployeeSurname = "Doe",
                PermissionTypeId = 1,
                PermissionType = permissionType,
                StartDate = DateTime.UtcNow
            };

            Assert.Equal("John", permission.EmployeeForename);
            Assert.Equal("Doe", permission.EmployeeSurname);
            Assert.Equal(1, permission.PermissionTypeId);
            Assert.Equal(permissionType, permission.PermissionType);
        }
    }
}
