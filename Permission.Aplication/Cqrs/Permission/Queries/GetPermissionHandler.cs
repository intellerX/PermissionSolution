using MediatR;
using Permission.Aplication.Cqrs.Permission.Dto;
using Permission.Domain.Ports;

namespace Permission.Aplication.Cqrs.Permission.Queries
{
    public class GetPermissionHandler : IRequestHandler<GetPermissionQuery, PermissionDto>, IDisposable
    {
        private readonly IUnitOfWork unitOfWork;

        public GetPermissionHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<PermissionDto> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException("request", "request requerido");

            var result = await unitOfWork.GetPermissionAsync(request.PermissionId);

            if (result == null)
                throw new ArgumentNullException("permiso no encotrado");

            return new PermissionDto
            {
                Id = result.Id,
                EmployeeForename = result.EmployeeForename,
                EmployeeSurname = result.EmployeeSurname,
                PermissionTypeId = result.PermissionTypeId,
                StartDate = result.StartDate
            };
        }
    }
}
