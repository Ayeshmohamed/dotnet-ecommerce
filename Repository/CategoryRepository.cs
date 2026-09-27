using Apps.Data;
using Apps.Dtos.Categories;
using Apps.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apps.Repository
{
    public class CategoryRepository
    {
        private readonly DatabaseContext _context;
        private readonly FilesUpload _filesUpload;
        public CategoryRepository(DatabaseContext context, FilesUpload filesUpload)
        {
            _context = context;
            _filesUpload = filesUpload;
        }

        public async Task<List<GetCategoriesDto>> GetCategories(FilterCategories request,CancellationToken cancellationToken)
        {
            IQueryable<Category> categories = _context.Categories.AsNoTracking();

            if (request.CategoryId is not null){
                
                categories = categories.Where(c => c.Id == request.CategoryId);
            }

            if (!string.IsNullOrEmpty(request.CategoryName))
            {
                categories = categories.Where(c=> c.Name == request.CategoryName);
            }

            if (request.ParentId is not null) {
                categories = categories.Where(c => c.ParentId == request.ParentId);
            }

            if(request.IsActive.HasValue)
            {
               categories = categories.Where(c =>  c.IsActive == request.IsActive);
            }

            return await categories.

                Skip((request.Page - 1) * request.PageSize).
                Take(request.PageSize).
                Select(category => new GetCategoriesDto
               {
                   Id = category.Id,
                   Name = category.Name,
                   Description = category.Description,
                   Image = category.Image,
                   IsActive = category.IsActive,
                   CreatedAt = category.CreatedAt,
                   UpdatedAt = category.UpdatedAt,
                   Parent = category.Parent != null ? new GetCategoryDto
                   {
                       Id = category.Parent.Id,
                       Name = category.Parent.Name,
                       Description = category.Parent.Description,
                       Image = category.Parent.Image,
                       IsActive = category.Parent.IsActive,
                       CreatedAt = category.Parent.CreatedAt,
                       UpdatedAt = category.Parent.UpdatedAt,
                   } : null,
               }).
               
               ToListAsync(cancellationToken);

        }

        public async Task<Category> GetCategoryById(int id) {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c=> c.Id == id);
        }

        public  async Task DeleteCategory(int id){
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var category = _context.Categories.Find(id);
                if (category is not null)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                }
              await transaction.CommitAsync();
            }
            catch (Exception ex) {
                await transaction.RollbackAsync();
            }
        }

        public async Task StoreCateogory(CreateCategoryDto data)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                string? ImagePath = null;

                if (data.Image != null && data.Image.Length > 0)
                {
                    ImagePath = _filesUpload.UploadImages(data.Image.FileName, "categories", data.Image);
                }

                Category category = new()
                {
                    Name = data.Name,
                    Description = data.Description,
                    Image = ImagePath,
                    ParentId = data.ParentId,
                    IsActive = data.IsActive,
                    CreatedAt = DateTime.Now
                };


                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task UpdateCategory(int id, UpdateCategoryDto data) {

            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var category = await _context.Categories.FindAsync(id);

                if (category is not null)
                {
                    string? ImagePath = null;

                    if (data.Image != null && data.Image.Length > 0)
                    {
                        ImagePath = _filesUpload.UploadImages(data.Image.FileName, "categories", data.Image);
                    }

                    category.Name = data.Name;
                    category.Description = data.Description;
                    category.Image = ImagePath;
                    category.ParentId = data.ParentId;
                    category.IsActive = data.IsActive;
                    category.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();
            }
             catch (Exception e)
            {
                await transaction.RollbackAsync();
            }
            
            
        }

    }
}
