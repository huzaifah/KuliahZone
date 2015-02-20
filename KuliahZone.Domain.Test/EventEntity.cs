using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KuliahZone.Domain.Entity;
using System.Collections.Generic;
using KuliahZone.Domain.Enums;
using KuliahZone.Domain.Interfaces;
using FakeItEasy;
using System.Linq;
using AutoMapper;
using KuliahZone.Domain.Dto;

namespace KuliahZone.Domain.Test
{
    [TestClass]
    public class EventEntity
    {
        private IEventRepository eventRepository;
        private EventService eventService;
        private string venueId;

        [TestInitialize]
        public void Initialize()
        {
            eventRepository = A.Fake<IEventRepository>();
            eventService = new EventService(eventRepository);
            
            Mapper.CreateMap<Event, EventDto>();
            Mapper.CreateMap<VenueDto, Venue>();
            Mapper.CreateMap<Book, BookDto>();
            Mapper.CreateMap<Schedule, ScheduleDto>();
            Mapper.CreateMap<Speaker, SpeakerDto>();

            Mapper.AssertConfigurationIsValid();

            var eventSpeaker = new List<EventDto>();
            var myEvent = new Event
                {
                    Title = "My Event",
                    Description = "My Event description",
                    PublishedOn = new DateTime(2014, 10, 2), 
                    ContributedBy = "User1"
                };

            venueId = Guid.NewGuid().ToString();
            var venue = new Venue { VenueID = venueId, Location = "Masjid Hasanah", State = "Selangor", Town = "Kajang", Name = "Masjid Hasanah" };
            myEvent.Venue = venue;

            myEvent.AddTopics(new string[]{ "Hadith" });

            myEvent.AddSchedule(DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10), RecurringFlag.Weekly);

            var eventDto = Mapper.Map<EventDto>(myEvent);

            eventSpeaker.Add(eventDto);            

            A.CallTo(() => eventRepository.GetUpcomingEventsBySpeaker("SpeakerA"))
                .Returns(eventSpeaker);

            A.CallTo(() => eventRepository.GetEventById("1"))
                .Returns(new EventDto { Title = "Event #1", Description = "Event #1 description" });

            A.CallTo(() => eventRepository.GetUpcomingEventsByVenue(venueId))
                .Returns(eventSpeaker);

            A.CallTo(() => eventRepository.GetUpcomingEventsByTopic("Hadith"))
                .Returns(eventSpeaker);

            A.CallTo(() => eventRepository.GetUpcomingEventsByUser("User1"))
                .Returns(eventSpeaker);

            A.CallTo(() => eventRepository.GetEventSchedules("TEST"))
                .Returns(eventSpeaker[0].EventSchedules);
        }

        [TestMethod]
        public void SubmitEvent_ValidEvent_EventSave()
        {
            var ev = new Event();

            ev.Title = "My New Event";
            ev.Description = "My new event would be held";
            ev.ContributedBy = "Huzaifah";
            ev.IsActive = true;
            ev.Venue = new Venue { Location = "Masjid Jamek", Town = "Kajang", State = "Selangor" };

            ev.AddSchedule(DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10), RecurringFlag.Weekly);
            ev.AddTopics(new string[] { "Aqidah", "Hadith" });
            ev.AddSpeaker("Ustaz Haslin", Guid.NewGuid().ToString());
            ev.AddSpeaker("Ustaz Bad");

            ev.AddBooks(new Book { Title = "Hadith 40", Author = "Imam Nawawi" });
            ev.AddTags(new string[] { "hadith", "aqidah" });

            var eventId = eventService.SubmitNewEvent(ev);

            Assert.IsInstanceOfType(ev.EventSchedules, typeof(IReadOnlyList<Schedule>));
            Assert.IsTrue(ev.EventSchedules.Count != 0);
            Assert.IsTrue(ev.EventTopics.Count == 2);

            Guid guidOut;
            Assert.IsTrue(Guid.TryParse(ev.EventID, out guidOut));

            Assert.IsTrue(ev.EventSpeakers.Count == 2);

        }

        [TestMethod]
        public void GetUpcomingEvents_BySpeaker_ReturnsEvents()
        {
            var events = eventService.GetUpcomingEventsBySpeaker("SpeakerA");

            Assert.IsInstanceOfType(events, typeof(List<Event>));
            Assert.IsTrue(events.Count() == 1);
        }

        [TestMethod]
        public void GetSingleEvent_ByEventId_ReturnEvent()
        {
            string eventId = "1";
            var eventDetails = eventService.GetSingleEvent(eventId);

            Assert.IsInstanceOfType(eventDetails, typeof(Event));
        }

        [TestMethod]
        public void GetUpcomingEvents_ByVenue_ReturnsEvents()
        {
            var eventDetails = eventService.GetUpcomingEventsByVenue(venueId);

            Assert.IsInstanceOfType(eventDetails, typeof(IEnumerable<Event>));

            foreach (var ev in eventDetails)
            {
                Assert.AreEqual(ev.Venue.VenueID, venueId);
            }
        }

        [TestMethod]
        public void GetUpcomingEvents_ByTopic_ReturnsEvents()
        {
            var topic = "Hadith";
            var eventDetails = eventService.GetUpcomingEventsByTopic(topic);

            Assert.IsInstanceOfType(eventDetails, typeof(IEnumerable<Event>));

            foreach (var ev in eventDetails)
            {
                Assert.IsTrue(ev.EventTopics.Contains(topic));
            }
        }

        [TestMethod]
        public void GetUpcomingEvents_ByCreatedBy_ReturnsEvents()
        {
            var user = "User1";
            var eventDetails = eventService.GetUpcomingEventsByUser(user);

            Assert.IsInstanceOfType(eventDetails, typeof(IEnumerable<Event>));

            foreach (var ev in eventDetails)
            {
                Assert.AreEqual(user, ev.ContributedBy);
            }
        }

        [TestMethod]
        public void GetEventSchedule_ByEventId_ReturnsSchedule()
        {
            var eventId = "TEST";

            var schedules = eventService.GetEventSchedules(eventId);

            Assert.IsInstanceOfType(schedules, typeof(IEnumerable<Schedule>));
            Assert.IsTrue(schedules.Count() == 1);

        }
    }
}
