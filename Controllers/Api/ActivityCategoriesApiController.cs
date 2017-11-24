using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZenithWebsite.Data;
using ZenithWebsite.Models;
using Microsoft.AspNetCore.Cors;

namespace ZenithWebsite.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/ActivityCategoriesApi")]
    [EnableCors("AllowApi")]
    public class ActivityCategoriesApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityCategoriesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ActivityCategoriesApi
        [HttpGet]
        public IEnumerable<ActivityCategory> GetActivityCategories()
        {
            return _context.ActivityCategories;
        }

        // GET: api/ActivityCategoriesApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activityCategory = await _context.ActivityCategories.SingleOrDefaultAsync(m => m.ActivityCategoryId == id);

            if (activityCategory == null)
            {
                return NotFound();
            }

            return Ok(activityCategory);
        }

        // PUT: api/ActivityCategoriesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityCategory([FromRoute] int id, [FromBody] ActivityCategory activityCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activityCategory.ActivityCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(activityCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ActivityCategoriesApi
        [HttpPost]
        public async Task<IActionResult> PostActivityCategory([FromBody] ActivityCategory activityCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ActivityCategories.Add(activityCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityCategory", new { id = activityCategory.ActivityCategoryId }, activityCategory);
        }

        // DELETE: api/ActivityCategoriesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activityCategory = await _context.ActivityCategories.SingleOrDefaultAsync(m => m.ActivityCategoryId == id);
            if (activityCategory == null)
            {
                return NotFound();
            }

            _context.ActivityCategories.Remove(activityCategory);
            await _context.SaveChangesAsync();

            return Ok(activityCategory);
        }

        private bool ActivityCategoryExists(int id)
        {
            return _context.ActivityCategories.Any(e => e.ActivityCategoryId == id);
        }
    }
}