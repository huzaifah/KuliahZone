using KuliahZone.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuliahZone.Domain.Entity
{
    public class Event
    {
        public string EventID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ContributedBy { get; set; }
        public DateTime? PublishedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public bool IsActive { get; set; }
        public Venue Venue { get; set; }

        private List<string> Tags { get; set; }

        private List<Speaker> Speakers { get; set; }
        private List<string> Topics { get; set; }
        private List<Book> Books { get; set; }

        private List<Schedule> Schedules { get; set; }

        public IReadOnlyList<Schedule> EventSchedules
        {
            get { return Schedules; }
        }

        public IReadOnlyList<string> EventTopics
        {
            get { return Topics; }
        }

        public IReadOnlyList<Speaker> EventSpeakers
        {
            get { return Speakers; }
        }

        public IReadOnlyList<Book> EventBooks
        {
            get { return Books; }
        }

        public IReadOnlyList<string> EventTags
        {
            get { return Tags; }
        }
        
        public Event()
        {
            Schedules = new List<Schedule>();
            Speakers = new List<Speaker>();
            Topics = new List<string>();
            Tags = new List<string>();
            Books = new List<Book>();

            if (String.IsNullOrEmpty(EventID))
            {
                EventID = Guid.NewGuid().ToString();
            }
        }

        public void AddSchedule(DateTime fromDate, DateTime untilDate, RecurringFlag recurringFlag)
        {
            var schedule = new Schedule();
            schedule.FromDate = fromDate;
            schedule.UntilDate = untilDate;
            schedule.Recurring = recurringFlag;

            Schedules.Add(schedule);
        }

        public void AddTopics(string[] topics)
        {
            Topics.AddRange(topics);
        }

        public void AddSpeaker(string speaker, string speakerId = "")
        {
            var speakerDetails = new Speaker();

            if (String.IsNullOrEmpty(speakerId))
            {
                speakerDetails.SpeakerID = Guid.NewGuid().ToString();
            }
            else
            {
                Guid guidTest;
                if (!Guid.TryParse(speakerId, out guidTest))
                    throw new ApplicationException("Not a valid speaker id");
                else
                    speakerDetails.SpeakerID = speakerId;
            }
                        
            speakerDetails.Name = speaker;

            Speakers.Add(speakerDetails);
        }

        public void AddBooks(Book bookToAdd)
        {
            if (String.IsNullOrEmpty(bookToAdd.BookID))
            {
                bookToAdd.BookID = Guid.NewGuid().ToString();
            }
            else
            {
                Guid guidTest;
                if (!Guid.TryParse(bookToAdd.BookID, out guidTest))
                    throw new ApplicationException("Not a valid book id");                
            }

            Books.Add(bookToAdd);
        }

        public void AddTags(string[] tags)
        {
            Tags.AddRange(tags);
        }
    }
}
