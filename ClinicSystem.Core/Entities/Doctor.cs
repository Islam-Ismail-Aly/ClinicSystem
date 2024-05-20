using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Core.Entities
{
    public class Doctor : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Schedule Schedule { get; set; }
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
