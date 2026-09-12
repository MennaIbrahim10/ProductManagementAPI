using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
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

                if (claimIdentity == null || !claimIdentity.IsAuthenticated)
                {
                    context.Result = new ForbidResult();
                }
                else
                {
                    var userId = int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var hasPermission = dbContext.userPermissions.Any(x => x.UserId == userId && x.PermissionId == attribute.Permission);

                    if(!hasPermission)
                        context.Result = new ForbidResult();
                }
            }

            
        }
    }
}
