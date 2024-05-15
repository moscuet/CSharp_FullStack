using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eshop.Core.src.Entity;
using Microsoft.AspNetCore.Authorization;

namespace Eshop.WebAPI.src.Middleware
{

    /* custom authorization
    - requirement
    - rule handler */

    public class VerifyResourceOwnerRequirement : IAuthorizationRequirement
    {
        public VerifyResourceOwnerRequirement()
        {
        }
    }

    public class VerifyResourceOwnerHandler : AuthorizationHandler<VerifyResourceOwnerRequirement, Address>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, VerifyResourceOwnerRequirement requirement, Address resource)
        {
            var claims = context.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; // id of authenticated user
            if(userId == resource.UserId.ToString())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

}