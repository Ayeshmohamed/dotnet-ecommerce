using Apps.Dtos.Categories;
using Apps.Entities;
using Apps.Repository;

namespace Apps.Services
{
    public interface ICategory
    {
         Task<List<GetCategoriesDto>> GetCategories(FilterCategories request,CancellationToken cancellationToken);

        Task<Category> GetCategoryById(int id);

        Task StoreCategory(CreateCategoryDto data);

        Task UpdateCategory(int id, UpdateCategoryDto data);

        void DeleteCategory(int id);

    }
}
