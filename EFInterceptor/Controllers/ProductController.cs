using EFInterceptor.Data;
using EFInterceptor.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFInterceptor.Controllers
{
    /// <summary>
    /// Product Controller
    /// </summary>
    /// <param name="db">Db</param>
    [ApiController]
    [Route("[controller]")]
    public class ProductController(DB db) : ControllerBase
    {
        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> All()
        {
            var result = await db.Products.Include(x => x.Category)
                .OrderBy(x => x.Name)
                .Select(x => new ProductOutput()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Category = x.Category.Name,
                    CategoryId = x.CategoryId,
                    LastUpdate = x.LastUpdate,
                    DeleteDate = x.DeleteDate,
                    CreationDate = x.CreationDate
                }).ToListAsync();

            return Ok(result);
        }

        /// <summary>
        /// Create new Product
        /// </summary>
        /// <param name="input">Product Information</param>
        /// <returns>Product Entity</returns>
        [HttpPost]
        public async Task<ActionResult> Add(ProductInput input)
        {
            var result = db.Products.Add(new Product()
            {
                Name = input.Name,
                Price = input.Price,
                CategoryId = input.CategoryId,
            });

            await db.SaveChangesAsync();
            return Ok(result.Entity);
        }

        /// <summary>
        /// Update a Product
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="input">Product Information</param>
        /// <returns>Product Entity</returns>
        [HttpPut]
        public async Task<ActionResult> Update(int id, ProductInput input)
        {
            var result = await db.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (result == null)
                return NotFound("Product Not Found");

            if (!await db.Categories.AnyAsync(c => c.Id == input.CategoryId))
                return NotFound("Category Not Found");

            result.Name = input.Name;
            result.Price = input.Price;
            result.CategoryId = input.CategoryId;

            await db.SaveChangesAsync();

            return Ok(result);
        }

        /// <summary>
        /// Delete a Product
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Product Entity</returns>
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await db.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (result == null)
                return NotFound();

            db.Products.Remove(result);

            await db.SaveChangesAsync();
            return Ok(result);
        }


    }



}
