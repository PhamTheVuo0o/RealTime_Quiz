using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using AppCore.Infrastructure.Common.Constants;
using AppCore.Infrastructure.Enums;
using AppCore.Infrastructure.Extensions;

namespace AppCore.Infrastructure.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public PermissionEnum Permission { get; set; }
        public PermissionDetailEnum Details { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var attrPermissionRestrictions = context.Filters.OfType<CustomAuthorizeAttribute>().ToList();

            if (attrPermissionRestrictions != null)
            {
                var permissionsFromToken = DecodeTokenAndGetPermissions(token);
                if (permissionsFromToken == null)
                {
                    context.Result = new ForbidResult();
                    return;
                }

                var permissionsInput = attrPermissionRestrictions.Select(x =>
                    $"{x.Permission.ToShort()}:" +
                    $"{x.Details.ToShort()}").ToList();

                var rlt = from item in permissionsInput
                          where permissionsFromToken.Contains(item)
                          select item;
                if (rlt == null)
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
        }

        private string[] DecodeTokenAndGetPermissions(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            // Extract permissions from claims
            var permissions = jsonToken.Claims
                .Where(c => c.Type == CoreConstant.CLAIM_PERMISSIONS)
                .Select(c => c.Value)
                .ToArray();

            return permissions;
        }
    }
}
