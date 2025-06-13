using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class UploadFile
    {
        public static Error UnexpectedError => Error.Unexpected(
            code: "UploadFile.UnexpectedError",
            description: "Could not process file.");
    }
}