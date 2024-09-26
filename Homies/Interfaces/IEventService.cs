
using Homies.Models;

namespace Homies.Interfaces
{
    public interface IEventService
    {
        Task AddEventAsync(EventFormViewModel model, DateTime start, DateTime end, string userId);

        Task<bool> AddEventParticipentAsync(int id, string userId);

        Task<IEnumerable<EventInfoViewModel>> GetAllAsync();

        Task<EventFormViewModel> GetByIdAsync(int id);

        Task<EventDetailsViewModel> GetDetailsById(int id);

        Task<IEnumerable<EventInfoViewModel>> GetJoinedEventsAsync(string userId);

        Task<IEnumerable<TypeViewModel>> GetTypesAsync();

        Task RemoveAsync(int id, string userId);

        Task UpdateAsync(EventFormViewModel model, DateTime start, DateTime end);
    }
}
