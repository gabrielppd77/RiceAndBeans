using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class RecoverPassword
    {
        public static Error InvalidToken => Error.Validation(
            code: "RecoverPassword.InvalidToken",
            description: "Invalid token.");

        public static Error ExpiredToken => Error.Validation(
            code: "RecoverPassword.ExpiredToken",
            description: "Expired token.");
    }
}