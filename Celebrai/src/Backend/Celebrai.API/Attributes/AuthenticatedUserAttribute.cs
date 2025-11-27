using Celebrai.API.Filters;
using Celebrai.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute(params RoleUsuario[] allowedRoles) : base(typeof(AuthenticatedUserFilter))
    {
        Arguments = new object[] { allowedRoles ?? Array.Empty<RoleUsuario>() };
    }
}
