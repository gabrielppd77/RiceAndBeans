namespace Contracts.Services.Authentication
{
    public interface IUserAuthenticated
    {
        public Guid GetUserId();
        public Guid GetCompanyId();
    }
}