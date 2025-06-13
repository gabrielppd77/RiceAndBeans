using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Company
    {
        public static Error CompanyNotFound => Error.NotFound(
            code: "User.CompanyNotFound",
            description: "Could not find company.");
    }
}