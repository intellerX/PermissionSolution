using MediatR;
using Microsoft.AspNetCore.Mvc;
using Permission.Aplication.Cqrs.Permission.Command;
using Permission.Aplication.Cqrs.Permission.Dto;
using Permission.Aplication.Cqrs.Permission.Queries;
using Permission.Aplication.Cqrs.PermissionType.Command;
using Permission.Aplication.Cqrs.PermissionType.Dto;

namespace PermissionApi.Controllers
{
    [ApiController]
    [Route("permission")]

    public class PermissionController
    {
        readonly IMediator mediator = default!;

        public PermissionController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("getPermission/{permissionId}")]
        public async Task<PermissionDto> Get(int permissionId) => await mediator.Send(new GetPermissionQuery(permissionId));

        [HttpPost("requestPermission")]
        public async Task<PermissionDto> Post(RequestPermissionCommand permission) => await mediator.Send(permission);

        [HttpPost("createPermissionType")]
        public async Task<PermissionTypeDto> PostPermissionType(AddPermissionTypeCommand permission) => await mediator.Send(permission);

        [HttpPut("updatePermission")]
        public async Task<PermissionDto> Put(UpdatePermissionCommand permission) => await mediator.Send(permission);
    }
}
