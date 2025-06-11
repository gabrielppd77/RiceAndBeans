using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Project
    {
        public static Error InvalidCredentials => Error.Forbidden(
            code: "Project.InvalidCred",
            description: "Invalid credentials.");
    }
}