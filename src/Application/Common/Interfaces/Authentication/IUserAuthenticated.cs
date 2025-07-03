namespace Application.Common.Interfaces.Authentication
{
    public interface IUserAuthenticated
    {
        public Guid GetUserId();
        public Guid GetCompanyId();
    }
}