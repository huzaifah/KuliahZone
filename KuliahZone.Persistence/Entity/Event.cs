using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuliahZone.Persistence.Entity
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid EventID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        
        public string ContributedBy { get; set; }
        public DateTime? PublishedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public bool Status { get; set; }
        public string Tag { get; set; }

        //public virtual Schedule Schedule { get; set; }
        //public virtual ICollection<Topic> Topics { get; protected set; }

        //public virtual ICollection<Book> Books { get; protected set; }

        //public virtual ICollection<Speaker> Speakers { get; protected set; }
        //public virtual Venue Venue { get; set; }

        public Event()
        {
            this.EventID = Guid.NewGuid();
            //this.Topics = new HashSet<Topic>();
            //this.Books = new HashSet<Book>();
            //this.Speakers = new HashSet<Speaker>();
        }
    }
}
