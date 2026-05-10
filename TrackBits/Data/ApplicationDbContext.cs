using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrackBits.Models.Entities;
using TrackBits.Models.Enums;

namespace TrackBits.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
        {
        }

  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<SerialNumber>()
    .HasOne(s => s.AssignedToUser)
    .WithMany()
    .HasForeignKey(s => s.AssignedToUserId)
    .OnDelete(DeleteBehavior.SetNull);



            modelBuilder.Entity<SerialNumber>().HasData(
    new SerialNumber
    {
        Id = 1,
        HashedSerial = "TRK-7AF9-2KLL-91PQ-77XZ",
        Status = SerialStatus.Unused,
        DurationInDays = 30,
        CreatedAt = new DateTime(2025, 1, 1),
        AssignedToUserId = null
    },
    new SerialNumber
    {
        Id = 2,
        HashedSerial = "TRK-9XZ1-PQL8-22KM-1D9M",
        Status = SerialStatus.Unused,
        DurationInDays = 30,
        CreatedAt = new DateTime(2025, 1, 1),
        AssignedToUserId = null
    },
    new SerialNumber
    {
        Id = 3,
        HashedSerial = "TRK-AB12-CD34-EF56-GH78",
        Status = SerialStatus.Unused,
        DurationInDays = 30,
        CreatedAt = new DateTime(2025, 1, 1),
        AssignedToUserId = null
    },
    new SerialNumber
    {
        Id = 4,
        HashedSerial = "TRK-X7LM-90QW-28PL-77JK",
        Status = SerialStatus.Unused,
        DurationInDays = 30,
        CreatedAt = new DateTime(2025, 1, 1),
        AssignedToUserId = null
    },
    new SerialNumber
    {
        Id = 5,
        HashedSerial = "TRK-55KL-A8P2-M1Q9-YZ33",
        Status = SerialStatus.Unused,
        DurationInDays = 30,
        CreatedAt = new DateTime(2025, 1, 1),
        AssignedToUserId = null
    }
);


        }




        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SerialNumber> SerialNumbers { get; set; }

    }
}
