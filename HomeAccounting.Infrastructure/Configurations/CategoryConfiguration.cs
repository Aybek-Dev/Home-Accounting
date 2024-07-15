using HomeAccounting.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeAccounting.Infrastructure.Configurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
	{
		public void Configure(EntityTypeBuilder<CategoryEntity> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
			builder.HasIndex(c => new { c.Name, c.UserId }).IsUnique();
			builder.HasOne(c => c.User)
				   .WithMany(u => u.Categories)
				   .HasForeignKey(c => c.UserId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
