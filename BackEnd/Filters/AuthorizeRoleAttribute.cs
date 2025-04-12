using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BackEnd.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AuthorizeRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _requiredRole;

        public AuthorizeRoleAttribute(string role)
        {
            _requiredRole = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var roleHeader = context.HttpContext.Request.Headers["UserRole"].ToString();

            if (string.IsNullOrEmpty(roleHeader) || !string.Equals(roleHeader, _requiredRole, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new ForbidResult(); // Devuelve 403 si no tiene permisos
            }
        }
    }
}

