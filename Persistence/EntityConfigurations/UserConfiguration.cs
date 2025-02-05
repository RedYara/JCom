using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> modelBuilder)
    {
        modelBuilder.HasOne(x => x.UserImage)
            .WithOne(x => x.User)
            .HasForeignKey<UserImage>(x => x.UserId);
    }
}