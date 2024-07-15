using HomeAccounting.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeAccounting.Infrastructure.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
	{
		public void Configure(EntityTypeBuilder<UserEntity> builder)
		{
			builder.HasKey(u => u.Id);
			builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
			builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
			builder.HasIndex(u => u.Email).IsUnique();
			builder.Property(u => u.PasswordHash).IsRequired();
			builder.HasMany(u => u.Transactions)
				   .WithOne(t => t.User)
				   .HasForeignKey(t => t.UserId)
				   .OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(u => u.Categories)
				   .WithOne(c => c.User)
				   .HasForeignKey(c => c.UserId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
