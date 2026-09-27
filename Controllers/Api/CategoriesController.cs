using Apps.Dtos.Categories;
using Apps.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apps.Controllers.Api
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        
        public CategoriesController (CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("index")]
        public async Task<IResult> Index([FromQuery]FilterCategories request, CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetCategories(request, cancellationToken);

            return Results.Ok(categories);
        }
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Detials(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category is null)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Category not found",
                    detail: $"Category with ID {id} was not found.");
            }

            GetCategoryDto categoryObject =  new GetCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Image = category.Image,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt,
            };

            return Ok(category);
        }

        [HttpPost("create")]
        public async Task<IResult> Create([FromForm]CreateCategoryDto data)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                        .Where(x => x.Value!.Errors.Any())
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();
                return Results.Json(errors.ToArray());
            }

            await _categoryService.StoreCategory(data);

            return Results.Ok("Category Created Successfully");
        }

        [HttpPost("update/{id}")]
        public async Task<IResult> Edit(int id, [FromForm] UpdateCategoryDto data)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                        .Where(x => x.Value!.Errors.Any())
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();
                return Results.Json(errors.ToArray());
            }

            await _categoryService.UpdateCategory(id,data);

            return Results.Ok("Category Updated Successfully");
        }

        [HttpPost("delete/{id}")]
        public async Task<IResult> Delete(int id)
        {
             _categoryService.DeleteCategory(id);

            return Results.Ok("Category Deleted Successfully");
        }
    }
}
