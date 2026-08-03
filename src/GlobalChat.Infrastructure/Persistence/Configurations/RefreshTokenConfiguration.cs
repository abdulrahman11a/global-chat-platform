using GlobalChat.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalChat.Infrastructure.Persistence.Configurations;
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.Property(r => r.Token).IsRequired().HasMaxLength(500);



        builder.HasOne(r => r.User)
       .WithMany(u => u.RefreshTokens)
       .HasForeignKey(r => r.UserId)
       .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => r.Token).IsUnique();

       
       // builder.HasOne<ApplicationUser>()
       //.WithMany(u => u.RefreshTokens)
       //.HasForeignKey(r => r.UserId)
       //.OnDelete(DeleteBehavior.Cascade);

    }
}
