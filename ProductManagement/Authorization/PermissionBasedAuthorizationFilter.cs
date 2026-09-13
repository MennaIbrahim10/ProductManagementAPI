using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductManagement.Data;
using System.Security.Claims;

namespace ProductManagement.Authorization
{
    public class PermissionBasedAuthorizationFilter(AppDbContext dbContext) : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var attribute = (PermissionBasedAuthorizationAttribute) context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is PermissionBasedAuthorizationAttribute);

            if(attribute != null)
            {
                var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;

                if (claimIdentity == null)
                {
                    context.Result = new ForbidResult();
                    return;
                }

                var userIdClaim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
                {
                    context.Result = new ForbidResult();
                    return;
                }

                var hasPermission = dbContext.userPermissions.Any(
                    x => x.UserId == userId && x.PermissionId == attribute.Permission
                );

                if (!hasPermission)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
