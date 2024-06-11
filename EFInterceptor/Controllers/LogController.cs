using EFInterceptor.Data;
using EFInterceptor.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFInterceptor.Controllers
{
    /// <summary>
    /// Log Controller
    /// </summary>
    /// <param name="db">Db</param>
    [ApiController]
    [Route("[controller]")]
    public class LogController(DB db) : ControllerBase
    {
        /// <summary>
        /// Get All Logs
        /// </summary>
        /// <returns>Logs</returns>
        [HttpGet]
        public async Task<ActionResult> All()
        {
            var result = await db.Logs
                .OrderByDescending(x => x.Date)
                .Select(x => new LogOutput()
                {
                    Id = x.Id,
                    State = x.State,
                    Date = x.Date,
                    Input = x.Input,
                    Detail = x.Detail
                }).ToListAsync();

            return Ok(result);
        }
    }
}
