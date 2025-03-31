using MediatR;
using Permission.Aplication.Cqrs.Permission.Dto;
using Permission.Domain.Services;

namespace Permission.Aplication.Cqrs.Permission.Command
{
    public class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, PermissionDto>
    {
        private readonly PermissionService permissionService;

        public UpdatePermissionHandler(PermissionService permissionService)
        {
            this.permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
        }

        public async Task<PermissionDto> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request),
                                        "Se necesita un objeto request para realizar esta Task");

            var permissionType = await permissionService.GetPermissionTypeAsync(request.PermissionTypeId);

            if (permissionType == null)
                throw new ArgumentException(nameof(permissionType), "El permissionType no existe");

            var permission = await permissionService.UpdatePermissionAsync(
                new Domain.Entities.PermissionEntity
                {
                    Id = request.PermissionId,
                    CreatedAt = DateTime.Now,
                    EmployeeForename = request.EmployeeForename,
                    EmployeeSurname = request.EmployeeSurname,
                    PermissionTypeId = request.PermissionTypeId,
                    StartDate = request.StartDate,
                    PermissionType = permissionType,
                }
            );

            return new PermissionDto {Id = permission.Id, EmployeeForename = permission.EmployeeForename, EmployeeSurname = permission.EmployeeSurname, PermissionTypeId = permission.PermissionTypeId };
        }
    }
}
