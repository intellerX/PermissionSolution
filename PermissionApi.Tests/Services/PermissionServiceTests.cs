using Moq;
using Permission.Domain.Entities;
using Permission.Domain.Ports;
using Permission.Domain.Services;
using Xunit;

namespace PermissionApi.Tests.Services
{
    public class PermissionServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly PermissionService _permissionService;

        public PermissionServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _permissionService = new PermissionService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task RequestPermissionAsync_ShouldCallUnitOfWork()
        {
            var permission = new PermissionEntity
            {
                EmployeeForename = "John",
                EmployeeSurname = "Doe",
                PermissionTypeId = 1,
                PermissionType = new PermissionTypeEntity { Description = "Test Type" },
                StartDate = DateTime.UtcNow
            };

            await _permissionService.RequestPermissionAsync(permission);

            _unitOfWorkMock.Verify(u => u.RequestPermissionAsync(permission), Times.Once);
        }

        [Fact]
        public async Task AddPermissionTypeAsync_ShouldReturnAddedPermissionType()
        {
            var description = "New Permission Type";
            var permissionType = new PermissionTypeEntity { Description = description };

            _unitOfWorkMock.Setup(u => u.AddPermissionTypeAsync(It.IsAny<PermissionTypeEntity>()))
                .ReturnsAsync(permissionType);

            var result = await _permissionService.AddPermissionTypeAsync(description);

            Assert.Equal(description, result.Description);
        }
    }
}
