using Apps.Data;
using Apps.Dtos.Products;
using Apps.Entities;
using Apps.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class ProductRepository 
{
    private readonly EcommerceContext _context;
    private readonly FilesUpload  _filesUpload;

    private readonly ProductImageRepository _productImageRepository;

    public ProductRepository(EcommerceContext context, FilesUpload filesUpload, ProductImageRepository productImageRepository)
    {
        _context = context;
        _filesUpload = filesUpload;
        _productImageRepository = productImageRepository;
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _context.Products.Include(p => p.Category).Include(p => p.ProductImages).ToListAsync();
    }

    public Task<Product?> GetProduct(int id)
    {
        return _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public void DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);

        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }

    public async Task StoreProduct(CreateProductDto data)
    {

        Product product = new()
        {
            Name = data.Name,
            Price = data.Price,
            Description = data.Description,
            CategoryId = data.CategoryId,
            Slug = data.Name,
            Sku  = data.Sku,
            DiscountPrice  = data.DiscountPrice,
            IsActive = data.IsActive,
            IsFeatured = data.IsFeatured,
            Quantity  = data.Quantity,
            Weight  = data.Weight,
            CreatedAt  = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };


        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        if (data.Images != null && data.Images.Count > 0)
        {
            foreach (var image in data.Images)
            {
              await  _productImageRepository.CreateProductImages(image, product.Id);
                
            }
        }

    }

    public async Task UpdateProduct(int id, UpdateProductDto data)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return;

        if (data.Images != null && data.Images.Count > 0)
        {
            foreach (var image in data.Images)
            {
                await _productImageRepository.CreateProductImages(image, product.Id);

            }
        }

        product.Name = data.Name;
        product.Price = data.Price;
        product.Description = data.Description;
        product.CategoryId = data.CategoryId;
        product.Slug = data.Name;
        product.Sku = data.Sku;
        product.DiscountPrice = data.DiscountPrice;
        product.IsActive = data.IsActive;
        product.IsFeatured = data.IsFeatured;
        product.Quantity = data.Quantity;
        product.Weight = data.Weight;
        product.CreatedAt = DateTime.Now;
        product.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
    }
}
