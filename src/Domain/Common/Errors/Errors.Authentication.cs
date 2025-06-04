using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvalidCred",
            description: "Invalid credentials.");

        public static Error EmailIsNotConfirmed => Error.Validation(
            code: "Auth.EmailIsNotConfirmed",
            description: "Email is not confirmed.");
    }
}