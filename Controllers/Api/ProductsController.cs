using Apps.Dtos.Products;
using Apps.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Apps.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }
        // GET: ProductsController
        [HttpGet("index")]
        public async Task<IResult> Index()
        {
            var products = await _productService.GetProducts();

            return Results.Ok(products);
        }

        [HttpGet("detail/{id}")]
        // GET: ProductsController/Details/5
        public async Task<IResult> Details(int id)
        {
            var product = await _productService.GetProduct(id);
                        
            return Results.Ok(product);
        }


        // POST: ProductsController/Create
        [HttpPost("create")]
        public async Task<IResult> Create([FromForm] CreateProductDto collection)
        {
                Console.WriteLine(collection);
                if (!ModelState.IsValid)
                {
                    var errors =  ModelState
                        .Where(x => x.Value!.Errors.Any())
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();
                return  Results.Json(errors.ToArray());
                }

                await _productService.StoreProduct(collection);

               return Results.Ok("Created Successfully");
            
        }


        // POST: ProductsController/Edit/5
        [HttpPost("update/{id}")]
        public async Task<IResult> Edit(int id,[FromForm] UpdateProductDto collection)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value!.Errors.Any())
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

            return   Results.Json(errors.ToArray());
            }

            await _productService.UpdateProduct(id,collection);

            return Results.Ok("Edit Done Successfully");

     
        }


        // POST: ProductsController/Delete/5
        [HttpPost("delete/{id}")]
        public async Task<IResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                _productService.DeleteProduct(id);
                return Results.Ok("Deleted Successfully");
            }
            catch
            {
                return Results.Ok("Try Again Latter");
            }
        }
    }
}
