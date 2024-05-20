using ClinicSystem.Core.Utilities;

namespace ClinicSystem.Core.Entities
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
}
