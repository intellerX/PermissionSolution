using MediatR;
using Permission.Aplication.Cqrs.Permission.Dto;
using System.ComponentModel.DataAnnotations;

namespace Permission.Aplication.Cqrs.Permission.Queries
{
    public record GetPermissionQuery([Required] int PermissionId) : IRequest<PermissionDto>;
}
