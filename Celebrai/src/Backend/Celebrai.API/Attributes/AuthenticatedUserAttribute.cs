using Celebrai.API.Filters;
using Celebrai.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Celebrai.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute(RoleUsuario role) : base(typeof(AuthenticatedUserFilter))
    {
        Arguments = new object[] { role };
    }
}
