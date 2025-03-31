using Permission.Domain.Entities;
using Xunit;

namespace PermissionApi.Tests.Entities
{
    public class PermissionTypeEntityTests
    {
        [Fact]
        public void PermissionTypeEntity_ShouldInitializeCorrectly()
        {
            var permissionType = new PermissionTypeEntity { Description = "Test Description" };

            Assert.Equal("Test Description", permissionType.Description);
        }
    }
}
