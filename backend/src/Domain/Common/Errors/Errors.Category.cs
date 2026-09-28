using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Category
    {
        public static Error CategoryNotFound => Error.NotFound(
            code: "Category.CategoryNotFound",
            description: "Could not find category.");
    }
}