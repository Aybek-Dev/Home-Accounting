using HomeAccounting.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeAccounting.Infrastructure.Configurations
{
	public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
	{
		public void Configure(EntityTypeBuilder<TransactionEntity> builder)
		{
			builder.HasKey(t => t.Id);
			builder.Property(t => t.Amount).IsRequired().HasColumnType("decimal(18,2)");
			builder.Property(t => t.Title).HasMaxLength(256);
			builder.HasOne(t => t.User)
				   .WithMany(u => u.Transactions)
				   .HasForeignKey(t => t.UserId)
				   .OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(t => t.Category)
				   .WithMany()
				   .HasForeignKey(t => t.CategoryId)
				   .OnDelete(DeleteBehavior.Restrict);
			builder.Navigation(c => c.Category).AutoInclude();
		}
	}
}
