using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackBits.Models.Entities;

namespace TrackBits.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            //Handle Uniqe Email
            builder.HasIndex(user => user.Email)
                .IsUnique();

            //required fields
            builder.Property(user => user.Email)
                .IsRequired().HasMaxLength(250);


            //user to Payments numbers relationship One to Many 
            builder.HasMany(user => user.Payments)
                .WithOne(payment => payment.User)
                .HasForeignKey(payment => payment.UserId);




            //user to Serial relationship One to Many

            builder.HasMany(user => user.SerialNumbers)
                .WithOne(serial => serial.AssignedToUser)
                .HasForeignKey(serial => serial.AssignedToUserId);


            //default values
            builder.Property(user => user.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(user => user.IsActive)
           .HasDefaultValue(true);




        }






    }
}
