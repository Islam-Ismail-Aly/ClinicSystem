namespace ClinicSystem.Core.Entities
{
    public class Secretary : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
