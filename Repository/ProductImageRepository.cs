using Apps.Data;
using Apps.Dtos.Products;
using Apps.Entities;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Apps.Repository
{
    public class ProductImageRepository
    {
        private readonly DatabaseContext _context;
        private readonly FilesUpload _filesUpload;
        public ProductImageRepository(DatabaseContext context, FilesUpload filesUpload) { 
            _context = context;
            _filesUpload = filesUpload;
        }


        public async Task CreateProductImages(CreateProductImageDto image,int ProductId)
        {
            if (image is not null)
            {
               
                var  ImagePath = _filesUpload.UploadImages(image.Image.FileName, "products", image.Image);
                ProductImage productImage = new ProductImage()
                {

                    ProductId = ProductId,
                    ImageUrl = ImagePath,
                    IsMain = image.IsMain,
                    CreatedAt = DateTime.Now,
                };
                await _context.ProductImages.AddAsync(productImage);
                await _context.SaveChangesAsync();
            }
         }
    }
}
