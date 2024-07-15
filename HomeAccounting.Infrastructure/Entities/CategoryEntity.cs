using HomeAccounting.Domain.Common.BaseEntities.BaseGuidEntities;

namespace HomeAccounting.Infrastructure.Entities
{
	public class CategoryEntity:BaseGuidAuditableEntity
	{
		public required string Name { get; set; }
        public required Guid UserId { get; set; }
        public UserEntity User { get; set; }
	}
}
