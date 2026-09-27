using Apps.Dtos.Categories;
using Apps.Entities;
using Apps.Repository;

namespace Apps.Services
{
    public class CategoryService : ICategory
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository) { 
            _categoryRepository = categoryRepository;
        }

        public async Task<List<GetCategoriesDto>> GetCategories(FilterCategories request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetCategories(request,cancellationToken);
        }

        public async Task<Category> GetCategoryById(int id) { 
            var category = await _categoryRepository.GetCategoryById(id);

            return category;
        
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
