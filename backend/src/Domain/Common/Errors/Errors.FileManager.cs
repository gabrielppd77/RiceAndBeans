using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class FileManager
    {
        public static Error BucketNotFound => Error.NotFound(
            code: "FileManager.BucketNotFound",
            description: "Could not find bucket.");

        public static Error UnexpectedError => Error.Unexpected(
            code: "FileManager.UnexpectedError",
            description: "Could not process file.");
    }
}