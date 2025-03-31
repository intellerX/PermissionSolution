using MediatR;
using Permission.Aplication.Cqrs.PermissionType.Dto;
using Permission.Domain.Services;

namespace Permission.Aplication.Cqrs.PermissionType.Command
{
    public class AddPermissionTypeHandler : IRequestHandler<AddPermissionTypeCommand, PermissionTypeDto>
    {
        private readonly PermissionService permissionService;

        public AddPermissionTypeHandler(PermissionService permissionService)
        {
            this.permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
        }

        public async Task<PermissionTypeDto> Handle(AddPermissionTypeCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request),
                                        "Se necesita un objeto request para realizar esta Task");

            var permission = await permissionService.AddPermissionTypeAsync(
                request.Description
            );

            return new PermissionTypeDto { Id = permission.Id, Description = permission.Description };
        }
    }
}
