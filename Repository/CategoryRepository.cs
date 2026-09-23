using Apps.Data;
using Apps.Dtos.Categories;
using Apps.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apps.Repository
{
    public class CategoryRepository
    {
        private readonly EcommerceContext _context;
        private readonly FilesUpload _filesUpload;
        public CategoryRepository(EcommerceContext context, FilesUpload filesUpload)
        {
            _context = context;
            _filesUpload = filesUpload;
        }

        public async Task<List<Category>> GetCategories()
        {
           return await  _context.Categories.Include(c => c.Parent).ToListAsync();

        }

        public async Task<Category> GetCategoryById(int id) {
            return await _context.Categories.FindAsync(id);
        }

        public  void DeleteCategory(int id){
            var category =  _context.Categories.Find(id);
            if (category is not null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public async Task StoreCateogory(CreateCategoryDto data)
        {
            string? ImagePath = null;

            if (data.Image != null && data.Image.Length > 0)
            {
                ImagePath = _filesUpload.UploadImages(data.Image.FileName, "categories", data.Image);
            }

            Category category = new () { 
                Name = data.Name,
                Description = data.Description,
                Image= ImagePath,
                ParentId = data.ParentId,
                IsActive = data.IsActive,
                CreatedAt = DateTime.Now
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(int id, UpdateCategoryDto data) {
            var category = await _context.Categories.FindAsync(id);

            if (category is not null) {
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
            
        }

    }
}
