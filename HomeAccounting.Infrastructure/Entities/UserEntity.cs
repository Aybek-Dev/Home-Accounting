using HomeAccounting.Domain.Common.BaseEntities.BaseGuidEntities;

namespace HomeAccounting.Infrastructure.Entities
{
	public class UserEntity:BaseGuidAuditableEntity
	{
		public required string Name { get; set; } 
		public required string Email { get; set; }
		public required string PasswordHash { get; set; }
		public ICollection<TransactionEntity>? Transactions { get; set; }
        public ICollection<CategoryEntity>? Categories { get; set; }
    }
}
