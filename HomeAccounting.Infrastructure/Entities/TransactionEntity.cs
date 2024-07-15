using HomeAccounting.Domain.Common.BaseEntities.BaseGuidEntities;
using HomeAccounting.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HomeAccounting.Infrastructure.Entities
{
	public class TransactionEntity : BaseGuidAuditableEntity
	{
        public required Guid UserId { get; set; }
        public  UserEntity User { get; set; }
		public required TransactionType Type { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public  CategoryEntity Category { get; set; }
		public decimal Amount { get; set; }
		public string? Title { get; set; }
	}
}
