using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZenithWebsite.Data;
using ZenithWebsite.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace ZenithWebsite.Controllers
{
    public class ActivityCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IStringLocalizer<ActivityCategoriesController> _localizer;
        public ActivityCategoriesController(ApplicationDbContext context, IStringLocalizer<ActivityCategoriesController> localizer)
        {
            _localizer = localizer;
            _context = context;
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
            );

            return LocalRedirect(returnUrl);
        }

        // GET: ActivityCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActivityCategories.ToListAsync());
        }

        // GET: ActivityCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityCategory = await _context.ActivityCategories
                .SingleOrDefaultAsync(m => m.ActivityCategoryId == id);
            if (activityCategory == null)
            {
                return NotFound();
            }

            return View(activityCategory);
        }

        // GET: ActivityCategories/Create
        public IActionResult Create()
        {
            ViewData["Message"] = _localizer["Your application description page."];
            return View();
        }

        // POST: ActivityCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityCategoryId,ActivityDescription,CreationDate")] ActivityCategory activityCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activityCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(activityCategory);
        }

        // GET: ActivityCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityCategory = await _context.ActivityCategories.SingleOrDefaultAsync(m => m.ActivityCategoryId == id);
            if (activityCategory == null)
            {
                return NotFound();
            }
            return View(activityCategory);
        }

        // POST: ActivityCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityCategoryId,ActivityDescription,CreationDate")] ActivityCategory activityCategory)
        {
            if (id != activityCategory.ActivityCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activityCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityCategoryExists(activityCategory.ActivityCategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(activityCategory);
        }

        // GET: ActivityCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityCategory = await _context.ActivityCategories
                .SingleOrDefaultAsync(m => m.ActivityCategoryId == id);
            if (activityCategory == null)
            {
                return NotFound();
            }

            return View(activityCategory);
        }

        // POST: ActivityCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activityCategory = await _context.ActivityCategories.SingleOrDefaultAsync(m => m.ActivityCategoryId == id);
            _context.ActivityCategories.Remove(activityCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityCategoryExists(int id)
        {
            return _context.ActivityCategories.Any(e => e.ActivityCategoryId == id);
        }
    }
}
