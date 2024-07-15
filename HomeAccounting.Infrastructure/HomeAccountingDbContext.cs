using HomeAccounting.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Infrastructure
{
	public class HomeAccountingDbContext : DbContext
	{
		public HomeAccountingDbContext(DbContextOptions<HomeAccountingDbContext> options)
			: base(options)
		{
		}
		public DbSet<UserEntity> Users { get; set; }
		public DbSet<CategoryEntity> Categories { get; set; }
		public DbSet<TransactionEntity> Transactions { get; set; }
	}
}
