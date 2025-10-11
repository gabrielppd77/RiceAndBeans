using System.Security.Claims;
using Audit.Core;

namespace Api.Configurations.Audit;

public class CustomAuditScopeFactory : AuditScopeFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomAuditScopeFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override void OnScopeCreated(AuditScope auditScope)
    {
        var httpContext = _httpContextAccessor?.HttpContext;

        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var user = httpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            auditScope.SetCustomField("AuthenticatedUserId", userId);
        }
    }
}