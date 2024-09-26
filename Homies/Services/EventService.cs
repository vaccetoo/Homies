using Homies.Data;
using Homies.Data.Models;
using Homies.Interfaces;
using Homies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static Homies.Data.Common.ValidationConstants;

namespace Homies.Services
{
    public class EventService : IEventService
    {
        private readonly HomiesDbContext _context;

        public EventService(HomiesDbContext context, UserManager<IdentityUser> user)
        {
            _context = context;
        }

        public async Task AddEventAsync(EventFormViewModel model, DateTime start, DateTime end, string userId)
        {
            var entity = new Event()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Start = start,
                End = end,
                CreatedOn = DateTime.Now,
                OrganaiserId = userId,
                TypeId = model.TypeId
            };

            await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddEventParticipentAsync(int id, string userId)
        {
            var @event = await _context.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if (@event == null)
            {
                return false;
            }

            if (!@event.EventsParticipants.Any(p => p.HelperId == userId))
            {
                @event.EventsParticipants.Add(new Data.Models.EventParticipant()
                {
                    HelperId = userId,
                    EventId = @event.Id
                });

                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<IEnumerable<EventInfoViewModel>> GetAllAsync()
        {
            return await _context.Events
                .Select(e => new EventInfoViewModel
                (
                    e.Id,
                    e.Name,
                    e.Start,
                    e.Type.Name,
                    e.Organaiser.UserName
                    ))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<EventFormViewModel> GetByIdAsync(int id)
        {
            var entity = await _context.Events
                .FindAsync(id);

            return new EventFormViewModel()
            {
                Id = id,
                Description = entity.Description,
                Start = entity.Start.ToString(DateFormat),
                End = entity.End.ToString(DateFormat),
                Name = entity.Name,
                TypeId = entity.TypeId,
                OrganaiserId = entity.OrganaiserId
            };
        }

        public async Task<EventDetailsViewModel> GetDetailsById(int id)
        {
            return await _context.Events
                .Where(e => e.Id == id)
                .AsNoTracking()
                .Select(e => new EventDetailsViewModel()
                {
                    CreatedOn = e.CreatedOn.ToString(DateFormat),
                    Description = e.Description,
                    End = e.End.ToString(DateFormat),
                    Id = id,
                    Name = e.Name,
                    Organiser = e.Organaiser.UserName,
                    Start = e.Start.ToString(DateFormat),
                    Type =e.Type.Name
                }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EventInfoViewModel>> GetJoinedEventsAsync(string userId)
        {
            return await _context.EventsParticipants
                .Where(ep => ep.HelperId == userId)
                .Select(ep => new EventInfoViewModel
                (
                    ep.EventId,
                    ep.Event.Name,
                    ep.Event.Start,
                    ep.Event.Type.Name,
                    ep.Event.Organaiser.UserName
                ))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TypeViewModel>> GetTypesAsync()
        {
            return await _context.Types
                .Select(t => new TypeViewModel()
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task RemoveAsync(int id, string userId)
        {
            var @event = await _context.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            var eventParticipent = @event.EventsParticipants
                .FirstOrDefault(ep => ep.HelperId == userId);

            if (eventParticipent != null)
            {
                @event.EventsParticipants.Remove(eventParticipent);

                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(EventFormViewModel model, DateTime start, DateTime end)
        {
            var entity = await _context.Events
                .FindAsync(model.Id);

            if(entity != null)
            {
                entity.Name = model.Name;
                entity.Start = start;
                entity.End = end;
                entity.Description = model.Description;
                entity.TypeId = model.TypeId;

                await _context.SaveChangesAsync();
            }
        }
    }
}
