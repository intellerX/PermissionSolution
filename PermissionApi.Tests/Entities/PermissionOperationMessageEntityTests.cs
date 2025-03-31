using Permission.Domain.Entities;
using Xunit;

namespace PermissionApi.Tests.Entities
{
    public class PermissionOperationMessageEntityTests
    {
        [Fact]
        public void PermissionOperationMessageEntity_ShouldInitializeCorrectly()
        {
            var operationMessage = new PermissionOperationMessageEntity
            {
                Operation = "Create",
                Payload = "Test Payload"
            };

            Assert.Equal("Create", operationMessage.Operation);
            Assert.Equal("Test Payload", operationMessage.Payload);
            Assert.NotEqual(Guid.Empty, operationMessage.Id);
        }
    }
}
