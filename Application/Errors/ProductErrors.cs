namespace Application.Errors;

public static class ProductErrors
{
    public static Error ValidationFailed(string Description) => new Error("Product.ValidationFailed", Description);

    public static Error NotFound(Guid guid) =>
        new Error("Product.NotFound", $"The product with id: {guid} was not found.");
    
    public static Error ProductExists(Guid guid) =>
        new Error("Product.ProductExists", $"The product with id: {guid} already exists.");
    public static Error CreateProductFailed(string Description) => new Error("Product.CreateProductFailed", Description);
    public static Error GetProductFailed(string Description) => new Error("Product.GetProductFailed", Description);
    public static Error DeleteProductFailed(string Description) => new Error("Product.DeleteProductFailed", Description);
    public static Error UpdateProductFailed(string Description) => new Error("Product.UpdateProductFailed", Description);
}