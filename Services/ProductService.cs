using Apps.Dtos;
using Apps.Dtos.Categories;
using Apps.Dtos.Products;
using Apps.Entities;


namespace Apps.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetProductsDto>> GetProducts()
        {
            var products = await _repository.GetProducts();

            return products.Select(p => new GetProductsDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Category = new GetCategoryDto { 
                    Id = p.Category.Id,
                    Name = p.Category.Name,
                    Description = p.Category.Description,
                    Image = p.Category.Image,

                },
                IsActive = p.IsActive,
                IsFeatured = p.IsFeatured,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                Slug = p.Slug,
                Sku = p.Sku,
                Weight = p.Weight,
                Quantity = p.Quantity,
                ProductImages = p.ProductImages,

            }).ToList();
        }

        public async Task<GetProductDto> GetProduct(int id)
        {
            var product = await _repository.GetProduct(id);
            if (product is null)
            {
                return null;
            }

            return new GetProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };

        }

        public async void DeleteProduct(int id)
        {
            _repository.DeleteProduct(id);
        }

        public async Task StoreProduct(CreateProductDto data)
        {
             await _repository.StoreProduct(data);
        }

        public async Task UpdateProduct(int id,UpdateProductDto data)
        {
            await _repository.UpdateProduct(id,data);
        }


    }
}
