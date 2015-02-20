using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuliahZone.Domain.Dto
{
    public class EventDto
    {
        public string EventID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ContributedBy { get; set; }
        public DateTime? PublishedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public bool IsActive { get; set; }
        public VenueDto Venue { get; set; }

        public IEnumerable<ScheduleDto> EventSchedules { get; set; }
        public IEnumerable<string> EventTopics { get; set; }
        public IEnumerable<SpeakerDto> EventSpeakers { get; set; }
        public IEnumerable<BookDto> EventBooks { get; set; }
        public IEnumerable<string> EventTags { get; set; }
    }
}
