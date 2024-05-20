using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Core.Entities
{
    public class ClinicTbl : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
