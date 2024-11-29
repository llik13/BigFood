using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.DAL.Entities
{
    public class DeliverSchedule
    {
        public int ScheduleId { get; set; }
        public string CourierId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        [Required]
        [EnumDataType(typeof(DayOfWeek))]
        public DayOfWeek DayOfWeek { get; set; }


        /*
          public string FirstName { get; set; }
        public string LastName { get; set; }
     
         */
    }
}
