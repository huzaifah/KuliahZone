using AutoMapper;
using KuliahZone.Domain.Dto;
using KuliahZone.Domain.Entity;
using KuliahZone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuliahZone.Domain
{
    public class EventService
    {
        private readonly IEventRepository eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;

            Mapper.CreateMap<Venue, VenueDto>();
            Mapper.CreateMap<ScheduleDto, Schedule>();

        }

        public Guid SubmitNewEvent(Event eventToSave)
        {
            return new Guid();
        }

        public IEnumerable<Event> GetUpcomingEventsBySpeaker(string speakerId)
        {
            var eventList = eventRepository.GetUpcomingEventsBySpeaker(speakerId);
            return Mapper.Map<List<Event>>(eventList);
        }

        public Event GetSingleEvent(string eventId)
        {
            var eventDto = eventRepository.GetEventById(eventId);
            return Mapper.Map<Event>(eventDto);
        }

        public IEnumerable<Event> GetUpcomingEventsByVenue(string venueId)
        {
            var eventList = eventRepository.GetUpcomingEventsByVenue(venueId);
            return Mapper.Map<List<Event>>(eventList);
        }

        public IEnumerable<Event> GetUpcomingEventsByTopic(string topic)
        {
            var eventList = eventRepository.GetUpcomingEventsByTopic(topic);
            return Mapper.Map<List<Event>>(eventList);
        }

        public IEnumerable<Event> GetUpcomingEventsByUser(string user)
        {
            var eventList = eventRepository.GetUpcomingEventsByUser(user);
            return Mapper.Map<List<Event>>(eventList);
        }

        public IEnumerable<Schedule> GetEventSchedules(string eventId)
        {
            var scheduleList = eventRepository.GetEventSchedules(eventId);
            return Mapper.Map<List<Schedule>>(scheduleList);
        }
    }
}
