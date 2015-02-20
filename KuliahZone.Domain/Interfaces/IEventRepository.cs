using KuliahZone.Domain.Dto;
using KuliahZone.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuliahZone.Domain.Interfaces
{
    public interface IEventRepository
    {
        IEnumerable<EventDto> GetUpcomingEventsBySpeaker(string speakerId);

        EventDto GetEventById(string eventId);

        IEnumerable<EventDto> GetUpcomingEventsByVenue(string venueId);

        IEnumerable<EventDto> GetUpcomingEventsByTopic(string topic);

        IEnumerable<EventDto> GetUpcomingEventsByUser(string user);

        IEnumerable<ScheduleDto> GetEventSchedules(string eventId);
    }
}
