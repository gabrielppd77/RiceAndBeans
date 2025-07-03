using System.Security.Claims;
using Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication
{
    public class UserAuthenticated : IUserAuthenticated
    {
        private readonly IHttpContextAccessor
            _httpContextAccessor;

        public UserAuthenticated(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var userIdentifier = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdentifier, out var userId) ? userId : Guid.Empty;
        }

        public Guid GetCompanyId()
        {
            var companyIdentifier =
                _httpContextAccessor?.HttpContext?.User?.FindFirst(CustomClaimTypes.CompanyId)?.Value;
            return Guid.TryParse(companyIdentifier, out var companyId) ? companyId : Guid.Empty;
        }
    }
}