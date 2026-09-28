using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Product
    {
        public static Error ProductNotFound => Error.NotFound(
            code: "Product.ProductNotFound",
            description: "Could not find product.");
    }
}