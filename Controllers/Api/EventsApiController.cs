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
using System.Globalization;

namespace ZenithWebsite.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/EventsApi")]
    [EnableCors("AllowApi")]
    public class EventsApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EventsApi
        [HttpGet]
        public Dictionary<string, List<Event>> GetEvents()
        {
            var dates = GetDaysOfCurrentWeek();
            List<ApiEventModel> apiEvents = new List<ApiEventModel>();
            var allEvents = _context.Events;
            var events = _context.Events.Include(e => e.ActivityCategory).Where(e => e.IsActive == true).ToList();

            // Sort the events by date time
            events.Sort((x, y) => x.StartDateTime.CompareTo(y.StartDateTime));

            Dictionary<string, List<Event>> dic = new Dictionary<string, List<Event>>();
            foreach (var d in dates)
            {
                dic.Add(d.ToString("D", new CultureInfo("EN-US")), new List<Event>());
                foreach (var e in events)
                {
                    if (e.StartDateTime.Date == d.Date)
                    {
                        dic[d.ToString("D", new CultureInfo("EN-US"))].Add(e);
                    }
                }
            }

            return dic;
            //foreach (var e in allEvents)
            //{
            //    apiEvents.Add(new ApiEventModel
            //    {
            //        Username = e.Username,
            //        EndDateTime = e.EndDateTime,
            //        EventId = e.EventId,
            //        CreationDate = e.CreationDate,
            //        IsActive = e.IsActive,
            //        StartDateTime = e.StartDateTime,
            //        ActivityCategory = _context.ActivityCategories.Find(e.ActivityCategoryId).ActivityDescription
            //    });
            //}
            //return apiEvents;
            
        }
        public static List<DateTime> GetDaysOfCurrentWeek()
        {
            DateTime startOfWeek = DateTime.Today.AddDays(
                ((int)(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek + 1)) -
                 (int)DateTime.Today.DayOfWeek);

            var result = Enumerable
              .Range(0, 7)
              .Select(i => startOfWeek
                 .AddDays(i)).ToList<DateTime>();
            return result;
            
        }
        // GET: api/EventsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _context.Events.SingleOrDefaultAsync(m => m.EventId == id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // PUT: api/EventsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent([FromRoute] int id, [FromBody] Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.EventId)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/EventsApi
        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.EventId }, @event);
        }

        // DELETE: api/EventsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _context.Events.SingleOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return Ok(@event);
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}