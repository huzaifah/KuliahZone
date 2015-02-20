using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuliahZone.Domain.Dto
{
    public class ScheduleDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? UntilDate { get; set; }
        public string Recurring { get; set; }
    }
}
