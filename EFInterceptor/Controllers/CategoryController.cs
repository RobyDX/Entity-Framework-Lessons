using EFInterceptor.Data;
using EFInterceptor.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFInterceptor.Controllers
{
    /// <summary>
    /// Category Controller
    /// </summary>
    /// <param name="db">Db</param>
    [ApiController]
    [Route("[controller]")]
    public class CategoryController(DB db) : ControllerBase
    {
        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> All()
        {
            var result = await db.Categories
                .OrderBy(x => x.Name)
                .Select(x => new CategoryOutput()
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastUpdate = x.LastUpdate,
                    DeleteDate = x.DeleteDate,
                    CreationDate = x.CreationDate
                }).ToListAsync();

            return Ok(result);
        }

        /// <summary>
        /// Create new Category
        /// </summary>
        /// <param name="input">Category Information</param>
        /// <returns>Category Entity</returns>
        [HttpPost]
        public async Task<ActionResult> Add(CategoryInput input)
        {
            var result = db.Categories.Add(new Category()
            {
                Name = input.Name,
            });

            await db.SaveChangesAsync();
            return Ok(result.Entity);
        }

        /// <summary>
        /// Update a Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <param name="input">Category Information</param>
        /// <returns>Category Entity</returns>
        [HttpPut]
        public async Task<ActionResult> Update(int id, CategoryInput input)
        {
            var result = await db.Categories.FirstOrDefaultAsync(p => p.Id == id);

            if (result == null)
                return NotFound("Category Not Found");

            result.Name = input.Name;

            await db.SaveChangesAsync();

            return Ok(result);
        }

        /// <summary>
        /// Delete a Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Category Entity</returns>
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await db.Categories.FirstOrDefaultAsync(p => p.Id == id);

            if (result == null)
                return NotFound();

            db.Categories.Remove(result);

            await db.SaveChangesAsync();
            return Ok(result);
        }
    }
}
