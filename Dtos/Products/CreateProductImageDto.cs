namespace Apps.Dtos.Products
{
    public record CreateProductImageDto
    (
        IFormFile Image,
        bool IsMain
    );
}
