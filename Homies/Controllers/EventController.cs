using Homies.Data.Common;
using Homies.Interfaces;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using static Homies.Data.Common.ValidationConstants;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<EventInfoViewModel> events = await _eventService.GetAllAsync();

            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            string userId = GetUserId();

            bool isAdded = await _eventService
                .AddEventParticipentAsync(id, userId);

            if (!isAdded)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            IEnumerable<EventInfoViewModel> model = await _eventService.GetJoinedEventsAsync(userId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new EventFormViewModel();

            model.Types = await _eventService.GetTypesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model)
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (!DateTime.TryParseExact(
                model.Start,
                DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(nameof(model.Start), InvalidDateFormat);
            }

            if (!DateTime.TryParseExact(
                model.End,
                DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out end))
            {
                ModelState.AddModelError(nameof(model.End), InvalidDateFormat);
            }

            if (!ModelState.IsValid)
            {
                model.Types = await _eventService.GetTypesAsync();

                return View(model);
            }

            await _eventService.AddEventAsync(model, start, end, GetUserId());

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EventFormViewModel model = await _eventService.GetByIdAsync(id);

            if (model == null)
            {
                return BadRequest();
            }

            if (model.OrganaiserId != GetUserId())
            {
                return Unauthorized();
            }

            model.Types = await _eventService.GetTypesAsync();

            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormViewModel model)
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (!DateTime.TryParseExact(
                model.Start,
                DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(nameof(model.Start), InvalidDateFormat);
            }

            if (!DateTime.TryParseExact(
                model.End,
                DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out end))
            {
                ModelState.AddModelError(nameof(model.End), InvalidDateFormat);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _eventService.UpdateAsync(model, start, end);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            await _eventService.RemoveAsync(id, GetUserId());

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            EventDetailsViewModel model = await _eventService.GetDetailsById(id);

            return View(model);
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
