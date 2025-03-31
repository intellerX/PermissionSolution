﻿using MediatR;
using Permission.Aplication.Cqrs.Permission.Dto;
using System.ComponentModel.DataAnnotations;

namespace Permission.Aplication.Cqrs.Permission.Command
{
    public record RequestPermissionCommand(
        [Required] string EmployeeForename,
        [Required] string EmployeeSurname,
        [Required] int PermissionTypeId,
        [Required] DateTime StartDate
    ) : IRequest<PermissionDto>;
}
