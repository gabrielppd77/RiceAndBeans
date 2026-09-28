using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class ConfirmEmail
    {
        public static Error InvalidToken => Error.Validation(
            code: "ConfirmEmail.InvalidToken",
            description: "Invalid token.");
    }
}