using ClinicSystem.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<ClinicTbl> Clinics { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Secretary> Secretaries { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Schedule)
                .WithOne(s => s.Doctor)
                .HasForeignKey<Schedule>(s => s.DoctorId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.ApplicationUser)
                .WithOne()
                .HasForeignKey<Doctor>(a => a.ApplicationUserId);

            modelBuilder.Entity<Secretary>()
                .HasOne(d => d.ApplicationUser)
                .WithOne()
                .HasForeignKey<Secretary>(a => a.ApplicationUserId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId);            
            
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Secretary)
                .WithMany()
                .HasForeignKey(a => a.SecretaryId);
        }
    }
}
