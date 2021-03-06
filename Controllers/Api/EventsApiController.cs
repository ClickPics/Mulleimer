﻿using System;
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
        public List<DateandEventsModel> GetEvents([FromQuery] string startDate)
        {
            DateTime start       = new DateTime();
            List<DateTime> dates = null;
            bool tryStart        = false;

            if (!startDate.Equals(""))
            {
                tryStart = DateTime.TryParse(startDate, out start);
            }

            dates = tryStart ? GetDaysOfCurrentWeekFromStart(start) : GetDaysOfCurrentWeek();

            List<ApiEventModel> apiEvents = new List<ApiEventModel>();
            var allEvents = _context.Events;
            var events    = _context.Events.Include(e => e.ActivityCategory).Where(e => e.IsActive == true).ToList();

            // Sort the events by date time
            events.Sort((x, y) => x.StartDateTime.CompareTo(y.StartDateTime));

            List<DateandEventsModel> datesEvents = new List<DateandEventsModel>();
            foreach (var d in dates)
            {
                DateandEventsModel currentModel = new DateandEventsModel();
                currentModel.DayOfWeek = (d.ToString("D", new CultureInfo("EN-US")));
                currentModel.Events = new List<ApiEventModel>();
                foreach (var e in events)
                {
                    if (e.StartDateTime.Date == d.Date)
                    {
                       currentModel.Events.Add(new ApiEventModel {
                            Username = e.Username,
                            EndDateTime = e.EndDateTime,
                            EventId = e.EventId,
                            CreationDate = e.CreationDate,
                            IsActive = e.IsActive,
                            StartDateTime = e.StartDateTime,
                            ActivityCategory = _context.ActivityCategories.Find(e.ActivityCategoryId).ActivityDescription
                        });
                    }
                }
                datesEvents.Add(currentModel);
            }

            return datesEvents;
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

        public static List<DateTime> GetDaysOfCurrentWeekFromStart(DateTime start)
        {
            return Enumerable.Range(0,7).Select(i => start.AddDays(i)).ToList<DateTime>();
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