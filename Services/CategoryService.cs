using Apps.Dtos.Categories;
using Apps.Repository;

namespace Apps.Services
{
    public class CategoryService : ICategory
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository) { 
            _categoryRepository = categoryRepository;
        }

        public async Task<List<GetCategoriesDto>> GetCategories(FilterCategories request)
        {
            return await _categoryRepository.GetCategories(request);
        }

        public async Task<GetCategoryDto> GetCategoryById(int id) { 
            var category = await _categoryRepository.GetCategoryById(id);

            return new GetCategoryDto { 
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Image = category.Image,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt,
            };
        
        }

        public async Task StoreCategory(CreateCategoryDto data) {

            await _categoryRepository.StoreCateogory(data);
        }

        public async Task UpdateCategory(int id,UpdateCategoryDto data)
        {
            await _categoryRepository.UpdateCategory(id,data);
        }

        public void DeleteCategory(int id) { 
            _categoryRepository.DeleteCategory(id);
        }
    }
}
