using MediatR;
using Permission.Aplication.Cqrs.PermissionType.Dto;
using System.ComponentModel.DataAnnotations;

namespace Permission.Aplication.Cqrs.PermissionType.Command
{
    public record AddPermissionTypeCommand(
               [Required] string Description
           ) : IRequest<PermissionTypeDto>;
}
