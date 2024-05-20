using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Core.Entities
{
    public class Schedule : BaseEntity
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DayOfWeek DayOff { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
