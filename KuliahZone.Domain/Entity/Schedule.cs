using KuliahZone.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuliahZone.Domain.Entity
{
    public class Schedule
    {
        public DateTime? FromDate { get; set; }
        public DateTime? UntilDate { get; set; }
        public RecurringFlag Recurring { get; set; }
    }
}
